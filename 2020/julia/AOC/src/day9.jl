module Day9
import AOC.Parsers

function check_validity(number, preamble)
	for adx in 1:length(preamble)
		if preamble[adx] > number 
			continue 
		end
		for bdx in adx:length(preamble)
			if number == preamble[adx] + preamble[bdx]
				return true 
			end
		end
	end
	false
end

function part1(;data=Parsers.day9(), preamble_len=25)
	for idx in (preamble_len + 1):length(data)
		if !check_validity(data[idx], @view data[(idx-preamble_len):(idx-1)])
			return data[idx]
		end
	end
end

function part2(;data=Parsers.day9(), preamble_len=25)
	target_number = part1(data=data, preamble_len=preamble_len)
	for right in length(data)-1:-1:2
		left = right - 1
		total = sum(@view data[left:right])
		while total < target_number 
			left -= 1
			total = sum(@view data[left:right])
		end
		if total == target_number 
			return sum(extrema(@view data[left:right]))
		end
	end
end

end