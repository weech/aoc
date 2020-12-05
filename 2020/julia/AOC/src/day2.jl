
module Day2

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

end


function day2_part1(;data=Parsers.day2())
	count(Day2.check_valid_password.(data))
end
function day2_part2(;data=Parsers.day2())
	count(Day2.check_valid_password2.(data))
end