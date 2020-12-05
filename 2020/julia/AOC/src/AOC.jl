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


function count_trees(slope, data)
	pos = [1, 1]
	count = 0
	while pos[1] < size(data, 1)
		pos .+= slope 
		count += data[pos[1], mod1(pos[2], size(data, 2))] 
	end
	count
end

struct Passport 
	byr::String
	iyr::String 
	eyr::String
	hgt::String
	hcl::String
	ecl::String
	pid::String
	cid::Union{Nothing, String}
end

function Passport(info::String)::Union{Missing,Passport}
	parts = Dict(map(split(info)) do item
		(key, value) = split(item, ':')
		key => value
	end)
	keys = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]
	if !all(map(k -> haskey(parts, k), keys))
		return missing 
	else
		if haskey(parts, "cid")
			Passport(map(k -> parts[k], keys)..., parts["cid"])
		else
			Passport(map(k -> parts[k], keys)..., nothing)
		end
	end
end

function validate_passport(card::Passport)::Bool
	!(1920 <= parse(Int, card.byr) <= 2002) && return false
	!(2010 <= parse(Int, card.iyr) <= 2020) && return false
	!(2020 <= parse(Int, card.eyr) <= 2030) && return false
	hgt = match(r"^(\d+)([a-z]+)$", card.hgt)
	if isnothing(hgt)
		return false
	elseif hgt.captures[2] == "in"
		!(59 <= parse(Int, hgt.captures[1]) <= 76) && return false
	elseif hgt.captures[2] == "cm"
		!(150 <= parse(Int, hgt.captures[1]) <= 193) && return false
	else 
		return false
	end
	!occursin(r"^#[[:xdigit:]]{6}$", card.hcl) && return false
	!(card.ecl in ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]) && return false
	!occursin(r"^\d{9}$", card.pid) && return false
	true
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

function day3_part1(;data=Parsers.day3())
	AOC.count_trees([1, 3], data)
end

function day3_part2(;data=Parsers.day3())
	mapreduce(*, [[1, 1], [1, 3], [1, 5], [1, 7], [2, 1]]) do slope
		AOC.count_trees(slope, data)
	end
end

function day4_part1(;data=Parsers.day4())
	count(!ismissing, map(AOC.Passport, data))
end

function day4_part2(;data=Parsers.day4())
	count(AOC.validate_passport, skipmissing(map(AOC.Passport, data)))
end

end # module Scripts
end # module AOC
