module AOC2019

include("parsers.jl")

mutable struct Program 
	memory::Vector{Int}
	instruction_pointer::Int
	finished::Bool
	input::Union{Channel{Int}, Nothing}
	output::Union{Channel{Int}, Nothing}
	relative_base::Int
end

const DATA_BUFFER = 1024

Program(data::Vector{Int}) = Program(vcat(data, zeros(DATA_BUFFER)), 1, false, nothing, nothing, 1)
Program(data::Vector{Int}, input, output) = Program(vcat(data, zeros(DATA_BUFFER)), 1, false, input, output, 1)

include("instructions.jl")

function execute_instruction!(program::Program)
	opcode = program.memory[program.instruction_pointer]
	#println("opcode: ", opcode, " jindex: ", program.instruction_pointer)
	instruction = get_instruction(opcode)
	execute!(instruction, program)
end

function execute_program!(program::Program)
	while !program.finished
		execute_instruction!(program)
	end
end

function fuel_required(mass) 
	fuel = (mass ÷ 3) - 2
	fuel <= 0 ? 0 : fuel
end

function fuel_required(mass, rocket::Bool)
	if mass <= 0 
		return 0
	end
	fuel = fuel_required(mass)
	fuel + fuel_required(fuel, true)
end

struct Point 
	x::Int 
	y::Int 
end

manhattan_distance(p::Point) = abs(p.x) + abs(p.y)

function expand_wire(wire_instructions)
	points = [Point(0, 0)]
	for instruction in wire_instructions
		direction = instruction[1]
		count = parse(Int, instruction[2:end])
		if direction == 'R'
			for c in 1:count 
				push!(points, Point(points[end].x+1, points[end].y))
			end
		elseif direction == 'U'
			for c in 1:count 
				push!(points, Point(points[end].x, points[end].y+1))
			end
		elseif direction == 'L'
			for c in 1:count 
				push!(points, Point(points[end].x-1, points[end].y))
			end
		elseif direction == 'D'
			for c in 1:count 
				push!(points, Point(points[end].x, points[end].y-1))
			end
		end
	end
	points
end

function find_closest_intersection(wire1, wire2)
	intersections = intersect(wire1, wire2)
	# Closest is the origin
	sort(manhattan_distance.(intersections))[2]
end

function find_shortest_intersection(wire1, wire2)
	intersections = intersect(wire1, wire2)
	works = map(intersections) do intersection 
		first = findfirst(x -> x == intersection, wire1) - 1
		second = findfirst(x -> x == intersection, wire2) - 1
		first + second
	end
	sort(works)[2]
end

function password_validator(number, min, max; part2=false)
	inrange = min <= number <= max 
	numdigits = floor(log10(number)) == 5
	if !(inrange && numdigits) 
		return false
	end
	digits = zeros(Int, 6)
	for place in 0:5
		digits[end-place] = number ÷ 10^place % 10
	end
	groups = [[digits[1]]]
	for digit in digits[2:end]
		if digit == groups[end][1]
			push!(groups[end], digit)
		else 
			push!(groups, [digit])
		end
	end
	comp = part2 ? (x, y) -> x == y : (x, y) -> x >= y
	has_double = comp(length(groups[1]), 2)
	all_increasing = true
	for gidx in 2:length(groups)
		if groups[gidx-1][1] > groups[gidx][1]
			all_increasing = false 
		end
		if comp(length(groups[gidx]), 2)
			has_double = true 
		end
	end
	has_double && all_increasing
end

mutable struct Node 
	name::String
	parent::Union{Node, Nothing}
	children::Vector{Node}
end

function depth(node::Node)
	if isnothing(node.parent)
		return 0
	else
		return 1 + depth(node.parent)
	end
end

add_child(parent::Node, child::Node) = push!(parent.children, child)

function check_behind(parent::Node, target::Node)
	if length(parent.children) == 0
		return false
	elseif target in parent.children
		return true
	else
		return any([check_behind(x, target) for x in parent.children])
	end
end

function move_up(you::Node)
	parent = you.parent 
	youidx = findfirst(x -> x == you, parent.children)
	deleteat!(parent.children, youidx)
	grandparent = parent.parent 
	you.parent = grandparent 
	add_child(grandparent, you)
end

function move_down(you::Node, target::Node)
	parent = you.parent 
	youidx = findfirst(x -> x == you, parent.children)
	deleteat!(parent.children, youidx)
	you.parent = target
	add_child(target, you)
end

is_sibling(you::Node, target::Node) = target in you.parent.children

function init_stream(values)
	input = Channel{Int}(32)
	for value in values
		put!(input, value)
	end
	input
end

has_duplicates(a, b, c, d, e) = length(Set([a, b, c, d, e])) != 5

function decode_dsn(data, width, height)
	numlayers = length(data) ÷ (width * height)
	layers = []
	for l in 1:numlayers 
		layer = zeros(width, height)
		section = parse.(Int, collect(data[((l-1) * width * height + 1):(l * width * height)]))
		push!(layers, permutedims(reshape(section, width, height)))
	end
	layers
end

module Scripts
	import AOC2019
	import AOC2019: fuel_required, Program, execute_program!, expand_wire
	import AOC2019: find_closest_intersection, find_shortest_intersection
	import AOC2019.Parsers
	import ColorTypes
	#import ImageView Comment out because this is slow

	function day1_part1()
		sum(fuel_required.(Parsers.day1()))
	end

	function day1_part2()
		sum(fuel_required.(Parsers.day1(), true))
	end

	function day2_part1() # 12490719
		data = Parsers.day2()
		data[2] = 12
		data[3] = 2
		program = Program(data)
		execute_program!(program)
		program.memory[1]
	end

	function day2_part2() #2003
		data = Parsers.day2()
		target = 19690720
		result = 0
		for noun in 0:99, verb in 0:99
			data[2] = noun
			data[3] = verb
			program = Program(copy(data))
			try
				execute_program!(program)
			catch e
				println("noun: ", noun, " verb: ", verb)
				println("data: ", data)
				println(program)
				error(e)
			end
			if program.memory[1] == target 
				result = 100 * noun + verb 
				break
			end
		end
		result
	end

	function day3_part1()
		wire1, wire2 = expand_wire.(Parsers.day3())
		find_closest_intersection(wire1, wire2)
	end

	function day3_part2()
		wire1, wire2 = expand_wire.(Parsers.day3())
		find_shortest_intersection(wire1, wire2)
	end

	function day4_part1()
		min = 307237
		max = 769058
		count(map(min:max) do value 
			AOC2019.password_validator(value, min, max)
		end)
	end

	function day4_part2()
		min = 307237
		max = 769058
		count(map(min:max) do value 
			AOC2019.password_validator(value, min, max, part2=true)
		end)
	end

	function day5_part1()
		data = Parsers.day5()
		input = AOC2019.init_stream([1])
		output = Channel{Int}(32)
		program = Program(data, input, output)
		task = @async execute_program!(program)
		wait(task)
		while isready(output)
			println(take!(output))
		end
	end

	function day5_part2()
		data = Parsers.day5()
		input = AOC2019.init_stream([5])
		output = Channel{Int}(32)
		program = Program(data, input, output)
		task = @async execute_program!(program)
		wait(task)
		while isready(output)
			println(take!(output))
		end
	end

	function day6_part1()
		nodes = Parsers.day6()
		reduce((x, y) -> x + AOC2019.depth(y), nodes, init=0)
	end

	function day6_part2()
		nodes = Parsers.day6()
		you = nodes[findfirst(x -> x.name == "YOU", nodes)]
		san = nodes[findfirst(x -> x.name == "SAN", nodes)]
		movecount = 0
		while !AOC2019.is_sibling(you, san)
			moved = false
			for sibling in you.parent.children
				if sibling != you
					if AOC2019.check_behind(sibling, san)
						AOC2019.move_down(you, sibling)
						movecount += 1
						moved = true
						break
					end
				end
			end
			if !moved 
				AOC2019.move_up(you)
				movecount += 1
			end
		end
		movecount
	end

	function day7_part1()
		data = Parsers.day7()
		running = 0
		range = 0:4
		for a in range, b in range, c in range, d in range, e in range
			if AOC2019.has_duplicates(a, b, c, d, e)
				continue
			end
			streams = [AOC2019.init_stream([a, 0])]
			for v in [b, c, d, e]
				push!(streams, AOC2019.init_stream([v]))
			end
			push!(streams, Channel{Int}(32))
			tasks = map(zip(streams[1:5], streams[2:end])) do (i, o) 
				program = Program(copy(data), i, o)
				@async execute_program!(program)
			end
			wait(streams[end])
			val = take!(streams[end])
			running = max(running, val)
		end
		running
	end

	function day7_part2()
		data = Parsers.day7()
		running = 0
		range = 5:9
		for a in range, b in range, c in range, d in range, e in range
			if AOC2019.has_duplicates(a, b, c, d, e)
				continue
			end
			streams = [AOC2019.init_stream([a, 0])]
			for v in [b, c, d, e]
				push!(streams, AOC2019.init_stream([v]))
			end
			tasks = map(zip(streams[1:5], vcat(streams[2:end], streams[1]))) do (i, o) 
				program = Program(copy(data), i, o)
				@async execute_program!(program)
			end
			wait(tasks[end])
			val = take!(streams[1])
			running = max(running, val)
		end
		running
	end

	function day8_part1()
		data = Parsers.day8()
		width, height = 25, 6
		layers = AOC2019.decode_dsn(data, width, height)
		tups = map(layers) do layer
			num_zeros = count(layer .== 0)
			num_ones = count(layer .== 1)
			num_twos = count(layer .== 2)
			num_zeros, num_ones * num_twos
		end
		sort!(tups, by = x -> x[1])
		tups[1][2]
	end

	function day8_part2()
		data = Parsers.day8()
		width, height = 25, 6
		layers = AOC2019.decode_dsn(data, width, height)
		rendering = layers[end]
		for layer in layers[end-1:-1:1]
			for idx in eachindex(layer)
				if layer[idx] == 1 || layer[idx] == 0
					rendering[idx] = layer[idx]
				end
			end
		end
		img = map(rendering) do v 
			if v == 0
				ColorTypes.RGBA(0, 0, 0, 1)
			elseif v == 1
				ColorTypes.RGBA(1, 1, 1, 1)
			else 
				ColorTypes.RGBA(0, 0, 0, 0)
			end
		end
		#Imageview.imshow(img) Too slow to have normally
		nothing
	end
end

end # module
