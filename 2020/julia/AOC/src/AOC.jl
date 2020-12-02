module AOC
include("parsers.jl")

check_entry(vals...) = sum(vals) == 2020 ? prod(vals) : 0

function check_valid_password(rulewordstr) 
	rule, word = split(rulewordstr, ':')
	rule_count, rule_letter = split(rule, ' ')
	atleast, atmost = parse.(Int, split(rule_count, '-'))
	rule_letter = only(strip(rule_letter))
	howmany = count(x -> x == rule_letter, word)
	atleast <= howmany <= atmost
end

function check_valid_password2(rulewordstr) 
	rule, word = split(rulewordstr, ':')
	word = strip(word)
	rule_idc, rule_letter = split(rule, ' ')
	idx1, idx2 = parse.(Int, split(rule_idc, '-'))
	rule_letter = only(strip(rule_letter))
	(word[idx1] == rule_letter) != (word[idx2] == rule_letter)
end

module Scripts
import AOC
import AOC.Parsers

function day1_part1(;data=Parsers.day1())
	for left in 1:length(data)
		for right in (left+1):length(data)
			guess = AOC.check_entry(data[left], data[right])
			if guess != 0
				return guess
			end
		end
	end
end

function day1_part2(;data=Parsers.day1())
	for item in Iterators.product(data, data, data)
		guess = AOC.check_entry(item...)
		if guess != 0
			return guess 
		end
	end
end

function day2_part1(;data=Parsers.day2())
	count(AOC.check_valid_password.(data))
end
function day2_part2(;data=Parsers.day2())
	count(AOC.check_valid_password2.(data))
end

end # module Scripts
end # module AOC
