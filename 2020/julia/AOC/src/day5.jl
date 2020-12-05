module Day5
get_seat_id(str) = parse(Int, "0b" * String(replace(collect(str), 'F'=>'0', 'B'=>'1', 'R'=>'1', 'L'=>'0')))
end

day5_part1(;data=Parsers.day5()) = maximum(Day5.get_seat_id, data)

function day5_part2(;data=Parsers.day5())
	sorted = sort(Day5.get_seat_id.(data))
	findfirst(x -> x == 2, diff(sorted)) + sorted[1]
end