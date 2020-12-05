
module Day1
check_entry(vals...) = sum(vals) == 2020 ? prod(vals) : 0
end

function day1_part1(;data=Parsers.day1())
	for left in 1:length(data)
		for right in (left+1):length(data)
			guess = Day1.check_entry(data[left], data[right])
			if guess != 0
				return guess
			end
		end
	end
end

function day1_part2(;data=Parsers.day1())
	for item in Iterators.product(data, data, data)
		guess = Day1.check_entry(item...)
		if guess != 0
			return guess 
		end
	end
end