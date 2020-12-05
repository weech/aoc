
module Day3
function count_trees(slope, data)
	pos = [1, 1]
	count = 0
	while pos[1] < size(data, 1)
		pos .+= slope 
		count += data[pos[1], mod1(pos[2], size(data, 2))] 
	end
	count
end
end


function day3_part1(;data=Parsers.day3())
	Day3.count_trees([1, 3], data)
end

function day3_part2(;data=Parsers.day3())
	mapreduce(*, [[1, 1], [1, 3], [1, 5], [1, 7], [2, 1]]) do slope
		Day3.count_trees(slope, data)
	end
end
