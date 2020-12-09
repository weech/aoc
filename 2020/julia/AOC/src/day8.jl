module Day8 
import AOC.Parsers

struct Program 
	accumulator::Int 
	idx::Int
end

function execute_instruction(instruction, number, state)
	if instruction == "acc"
		Program(state.accumulator + number, state.idx + 1)
	elseif instruction == "jmp"
		Program(state.accumulator, state.idx + number)
	else 
		Program(state.accumulator, state.idx + 1)
	end
end

function parse_instruction(str)
	instruction, number = split(str)
	instruction, parse(Int, number)
end

function trial(data)
	touched = Set{Int}()
	program = Program(0, 1)
	while (program.idx ∉ touched) && (program.idx <= length(data))
		push!(touched, program.idx)
		instruction, number = parse_instruction(data[program.idx])
		program = execute_instruction(instruction, number, program)
	end
	(program.accumulator, program.idx > length(data))
end

function part1(;data=collect(Parsers.day8()))
	trial(data)[1]
end

function part2(;data=collect(Parsers.day8()))

	jmps = findall(x -> occursin("jmp", x), data)
	for jidx in jmps
		run = copy(data)
		run[jidx] = replace(data[jidx], "jmp"=>"nop")
		acc, finished = trial(run)
		if finished
			return acc
		end
	end
	nops = findall(x -> occursin("nop", x), data)
	for nidx in nops
		run = copy(data)
		run[nidx] = replace(data[nidx], "nop"=>"jmp")
		acc, finished = trial(run)
		if finished
			return acc
		end
	end

end

end