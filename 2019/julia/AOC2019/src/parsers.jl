module Parsers

import AOC2019

function day1()::Vector{Int}
	lines = readlines(joinpath(@__DIR__, "..", "inputs", "day1.txt"))
	parse.(Int, lines)
end

function day2()::Vector{Int}
	line = readline(joinpath(@__DIR__, "..", "inputs", "day2.txt"))
	parse.(Int, split(line, ','))
end

function day3()
	lines = readlines(joinpath(@__DIR__, "..", "inputs", "day3.txt"))
	wire1 = split(lines[1], ',')
	wire2 = split(lines[2], ',')
	wire1, wire2
end

function day5()::Vector{Int}
	line = readline(joinpath(@__DIR__, "..", "inputs", "day5.txt"))
	parse.(Int, split(line, ','))
end

function day6()::Vector{AOC2019.Node}
	nodes = AOC2019.Node[]
	for line in readlines(joinpath(@__DIR__, "..", "inputs", "day6.txt"))
		parent, child = split(line, ')')
		exist_parent = findfirst(x -> x.name == parent, nodes)
		exist_child = findfirst(x -> x.name == child, nodes)
		if isnothing(exist_parent) && isnothing(exist_child)
			# Make a new root node 
			root = AOC2019.Node(parent, nothing, [])
			child_node = AOC2019.Node(child, root, [])
			AOC2019.add_child(root, child_node)
			push!(nodes, root)
			push!(nodes, child_node)
		elseif isnothing(exist_parent) && !isnothing(exist_child)
			# Add the parent to the child
			root = AOC2019.Node(parent, nothing, [nodes[exist_child]])
			nodes[exist_child].parent = root
			push!(nodes, root)
		elseif !isnothing(exist_parent) && isnothing(exist_child)
			# Add the child to the parent
			child_node = AOC2019.Node(child, nodes[exist_parent], [])
			AOC2019.add_child(nodes[exist_parent], child_node)
			push!(nodes, child_node)
		else 
			# Adding a newly discovered link 
			child_node = nodes[exist_child]
			root = nodes[exist_parent]
			AOC2019.add_child(root, child_node)
			child_node.parent = root
		end
	end
	nodes
end

function day7()::Vector{Int}
	line = readline(joinpath(@__DIR__, "..", "inputs", "day7.txt"))
	parse.(Int, split(line, ','))
end

function day8()::String 
	readline(joinpath(@__DIR__, "..", "inputs", "day8.txt"))
end

end