module Day14 
import AOC.Parsers

struct Mask
	orer::UInt64 
	ander::UInt64
	addresses::Vector{UInt64}
end

function Mask(str::AbstractString, part1::Bool)
	if part1
		orer = parse(UInt64, replace(str, 'X'=>'0'), base=2)
		ander = parse(UInt64, replace(str, 'X'=>'1'), base=2)
		Mask(orer, ander, [])
	else 
		orer = parse(UInt64, replace(str, 'X'=>'0'), base=2)
		float = parse(UInt64, replace(replace(str, '1'=>'0'), 'X'=>'1'), base=2)
		Mask(orer, float, expand_address_mask(float))
	end
end

Mask() = Mask(0, 0, [])

apply_mask(mask, value) = (value | mask.orer) & mask.ander

function expand_address_mask(float)
	addresses = UInt64[]
	count = 0
	notfloat = ~float
	baseitr = mod1(float, 2)
	while count <= float
		if (count & notfloat) == 0
			push!(addresses, count)
			count += baseitr
		else 
			count += count & notfloat
		end
	end
	addresses
end

decode_addresses(mask, value) = value | mask.ander ⊻ mask.ander .| mask.addresses .| mask.orer

struct Assignment
	target::UInt64
	value::UInt64
end

function Assignment(targetstr::AbstractString, valuestr::AbstractString)
	target = parse(UInt64, match(r"mem\[(\d+)\]", targetstr).captures[1])
	value = parse(UInt64, valuestr)
	Assignment(target, value)
end

mutable struct Program 
	mask::Mask 
	memory::Dict{UInt64, UInt64}
end

values(program::Program) = Base.values(program.memory)

Program() = Program(Mask(), Dict())

function execute_instruction!(program, inst; part1=true)
	splat = split(inst)
	if splat[1] == "mask"
		program.mask = Mask(splat[3], part1)
	else 
		assign = Assignment(splat[1], splat[3])
		if part1
			program.memory[assign.target] = apply_mask(program.mask, assign.value)
		else
			for address in decode_addresses(program.mask, assign.target)
				program.memory[address] = assign.value
			end
		end
	end
end

function part1(;data=Parsers.day14())
	program = Program()
	for instruction in data 
		execute_instruction!(program, instruction)
	end
	Int(sum(values(program)))
end

function part2(;data=Parsers.day14())
	program = Program()
	for instruction in data 
		execute_instruction!(program, instruction, part1=false)
	end
	Int(sum(values(program)))
end

end