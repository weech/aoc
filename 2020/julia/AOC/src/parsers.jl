module Parsers

const DATA = joinpath(@__DIR__, "..", "..", "..", "data")

function day1()
	parse.(Int, readlines(joinpath(DATA, "day1.txt")))
end

function day2()
	readlines(joinpath(DATA, "day2.txt"))
end

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

end # module