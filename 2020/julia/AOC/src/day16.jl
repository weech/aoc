module Day16 
import AOC.Parsers

function determine_ranges(rules)
	Dict(map(rules) do rule
		caps = match(r"(.+?)(?=:): (\d+)-(\d+) or (\d+)-(\d+)", rule).captures
		name = strip(caps[1])
		range1 = parse(Int, caps[2]):parse(Int, caps[3])
		range2 = parse(Int, caps[4]):parse(Int, caps[5])
		name => ((range1, range2))
	end)
end

function test_match(rules, item)
	for ranges in values(rules)
		if any(range -> item in range, ranges)
			return true 
		end
	end
	false
end

function part1(;data=Parsers.day16())
	rules, mine, nearby = map(x -> split(x, '\n', keepempty=false), data)
	rules = determine_ranges(rules)
	sum(nearby[2:end]) do ticket 
		sum(parse.(Int, split(ticket, ','))) do entry
			test_match(rules, entry) ? 0 : entry
		end
	end
end

function find_possible_rules(grid, idx, rules)
	ret = String[]
	for (key, ranges) in rules
		if all(row -> any(range -> row[idx] in range, ranges), grid)
			push!(ret, key)
		end
	end
	ret
end

# Because Dict doesn't play well with iteration functions 
function test_length_matches(pair)
	_, v = pair 
	length(v) > 1
end

function identify_columns(data)
	rules, mine, nearby = map(x -> split(x, '\n', keepempty=false), data)
	rules = determine_ranges(rules)
	tickets = map(x -> parse.(Int, split(x, ',')), nearby[2:end])
	valid_tickets = filter(tickets) do ticket 
		all(entry -> test_match(rules, entry), ticket)
	end
	possibles = Dict(map(1:length(valid_tickets[1])) do column 
		column=>find_possible_rules(valid_tickets, column, rules)
	end)
	assigned = Dict{String, Int}()
	while any(test_length_matches, possibles)
		for (index, matches) in possibles 
			if length(matches) == 1 
				assigned[matches[1]] = index
			else
				toremove = Int[] 
				for (idx, match) in enumerate(matches) 
					if haskey(assigned, match)
						push!(toremove, idx)
					end
				end
				deleteat!(matches, toremove)
			end
		end
	end
	# Misses the last one 
	for (idx, matches) in possibles 
		if !haskey(possibles, matches[1])
			assigned[matches[1]] = idx 
		end
	end
	mine = parse.(Int, split(mine[2], ','))
	# Because Dict does not play well with iteration functions 
	ret = Dict{String, Int}()
	for (key, value) in assigned 
		ret[key] = mine[value]
	end
	ret
end

function part2(;data=Parsers.day16())
	my_ticket = identify_columns(data)	
	# Because Dict does not play well with iteration functions
	subset = Int[]
	for (key, value) in my_ticket 
		if occursin("departure", key)
			push!(subset, value)
		end
	end
	prod(subset)
end

end