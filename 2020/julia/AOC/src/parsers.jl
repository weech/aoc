module Parsers

const DATA = joinpath(@__DIR__, "..", "..", "..", "data")

function day1()
	parse.(Int, readlines(joinpath(DATA, "day1.txt")))
end

function day2()
	readlines(joinpath(DATA, "day2.txt"))
end

end