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

end # module