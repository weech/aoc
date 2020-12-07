module AOC

export debug

function debug(item)
	println(item)
	item 
end

include("parsers.jl")

include("day1.jl")
include("day2.jl")
include("day3.jl")
include("day4.jl")
include("day5.jl")
include("day6.jl")
include("day7.jl")

end # module AOC