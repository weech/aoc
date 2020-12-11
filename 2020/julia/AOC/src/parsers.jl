module Parsers

const DATA = joinpath(@__DIR__, "..", "..", "..", "data")

day1() = parse.(Int, readlines(joinpath(DATA, "day1.txt")))

day2() = readlines(joinpath(DATA, "day2.txt"))

function day3()
	ret = map(readlines(joinpath(DATA, "day3.txt"))) do line
		[Int(x == '#') for x in strip(line)]
	end
	permutedims(reduce(hcat, ret))
end

function day4()
	items = String[]
	last_was_blank = true
	for line in eachline(joinpath(DATA, "day4.txt"), keep=true)
		if last_was_blank
			push!(items, line)
			last_was_blank = false
		elseif strip(line) == ""
			last_was_blank = true
		else
			items[end] *= line 
			last_was_blank = false
		end
	end
	items
end

day5() = eachline(joinpath(DATA, "day5.txt"))

function day6()
	items = Vector{Vector{String}}()
	last_was_blank = true
	for line in eachline(joinpath(DATA, "day6.txt"), keep=true)
		if last_was_blank
			push!(items, [strip(line)])
			last_was_blank = false
		elseif strip(line) == ""
			last_was_blank = true
		else
			push!(items[end], strip(line))
			last_was_blank = false
		end
	end
	items
end

day7() = eachline(joinpath(DATA, "day7.txt"))

day8() = eachline(joinpath(DATA, "day8.txt"))

day9() = parse.(Int, eachline(joinpath(DATA, "day9.txt")))

day10() = parse.(Int, eachline(joinpath(DATA, "day10.txt")))

end # module