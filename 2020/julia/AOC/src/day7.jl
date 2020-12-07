module Day7 
import AOC


function make_bag(str::String)
	words = split(str)
	color = join(words[1:2], " ")
	bag_half = split(join(words[5:end], " "), ",")
	if bag_half[1] == "no other bags."
		(color, [])
	else
		bags = map(bag_half) do rule 
			rwords = split(rule)
			num = parse(Int, rwords[1])
			rcolor = join(rwords[2:3], " ")
			(num, rcolor)
		end
		(color, bags)
	end
end

function part1(;data=AOC.Parsers.day7())
	desire = "shiny gold"
	bags = map(make_bag, data)
	canhold = Set{String}()
	startlen = -1
	while length(canhold) - startlen != 0
		startlen = length(canhold)
		for bag in bags 
			for rule in bag[2]
				if desire == rule[2] || any(canhold .== rule[2])
					push!(canhold, bag[1])
					break # for rule in bag[2]
				end
			end
		end
	end
	length(canhold)
end

function count_children(bags, desire)
	if length(bags[desire]) == 0 
		0
	else 
		sum(bags[desire]) do (inner_count, inner_name)
			inner_count + (inner_count * count_children(bags, inner_name))
		end
	end
end

function part2(;data=AOC.Parsers.day7())
	desire = "shiny gold"
	bags = Dict{String, Vector{Tuple{Int, String}}}()
	for line in data
		color, contents = make_bag(line)
		bags[color] = contents 
	end
	count_children(bags, desire)
end

end # module