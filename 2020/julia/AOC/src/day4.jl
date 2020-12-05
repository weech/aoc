module Day4
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
end 

function day4_part1(;data=Parsers.day4())
	count(!ismissing, map(Day4.Passport, data))
end

function day4_part2(;data=Parsers.day4())
	count(Day4.validate_passport, skipmissing(map(Day4.Passport, data)))
end