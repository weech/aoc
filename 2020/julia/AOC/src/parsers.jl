module Parsers

const DATA = joinpath(@__DIR__, "..", "..", "..", "data")

function day1()
	parse.(Int, readlines(joinpath(DATA, "day1.txt")))
end

end