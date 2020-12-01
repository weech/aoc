
# Interface:
# Int = num_param(type)
# Bool = has_output(type)
# Int = execute(type, param1, param2)
#

function read_values(prog, modes)
	map(enumerate(modes)) do (v, m)
		if m == 0
			return prog.memory[prog.memory[prog.instruction_pointer + v]+1]
		elseif m == 1
			return prog.memory[prog.instruction_pointer + v]
		elseif m == 2
			return prog.memory[prog.relative_base + v]
		end
	end
end

abstract type Instruction end

struct Addition <: Instruction 
	modes::Vector{Int}
end
num_param(typ::Addition) = 3
function execute!(typ::Addition, prog::Program) 
	values = read_values(prog, typ.modes)
	oplace = prog.memory[prog.instruction_pointer+num_param(typ)]
	prog.memory[oplace+1] = values[1] + values[2]
	prog.instruction_pointer += num_param(typ) + 1
end

struct Multiplication <: Instruction 
	modes::Vector{Int}
end
num_param(typ::Multiplication) = 3
function execute!(typ::Multiplication, prog::Program) 
	values = read_values(prog, typ.modes)
	oplace = prog.memory[prog.instruction_pointer+num_param(typ)]
	prog.memory[oplace+1] = values[1] * values[2]
	prog.instruction_pointer += num_param(typ) + 1
end
struct Halt <: Instruction end
num_param(typ::Halt) = 0
function execute!(typ::Halt, prog::Program) 
	prog.finished = true
	prog.instruction_pointer += num_param(typ) + 1
end

struct Input <: Instruction end 
num_param(typ::Input) = 1
function execute!(typ::Input, prog::Program)
	oplace = prog.memory[prog.instruction_pointer+num_param(typ)]
	prog.memory[oplace+1] = take!(prog.input)
	prog.instruction_pointer += num_param(typ) + 1
end

struct Output <: Instruction 
	modes::Vector{Int}
end
num_param(typ::Output) = 1
function execute!(typ::Output, prog::Program)
	values = read_values(prog, typ.modes)
	prog.instruction_pointer += num_param(typ) + 1
	put!(prog.output, values[1])
end

struct JumpIfTrue <: Instruction 
	modes::Vector{Int}
end
num_param(typ::JumpIfTrue) = 2
function execute!(typ::JumpIfTrue, prog::Program)
	values = read_values(prog, typ.modes)
	if values[1] != 0
		prog.instruction_pointer = values[2] + 1
	else
		prog.instruction_pointer += num_param(typ) + 1
	end
end

struct JumpIfFalse <: Instruction 
	modes::Vector{Int}
end
num_param(typ::JumpIfFalse) = 2
function execute!(typ::JumpIfFalse, prog::Program)
	values = read_values(prog, typ.modes)
	if values[1] == 0
		prog.instruction_pointer = values[2] + 1
	else
		prog.instruction_pointer += num_param(typ) + 1
	end
end

struct LessThan <: Instruction 
	modes::Vector{Int}
end
num_param(typ::LessThan) = 3
function execute!(typ::LessThan, prog::Program) 
	values = read_values(prog, typ.modes)
	oplace = prog.memory[prog.instruction_pointer+num_param(typ)]
	prog.memory[oplace+1] = values[1] < values[2] ? 1 : 0
	prog.instruction_pointer += num_param(typ) + 1
end

struct Equals <: Instruction 
	modes::Vector{Int}
end
num_param(typ::Equals) = 3
function execute!(typ::Equals, prog::Program) 
	values = read_values(prog, typ.modes)
	oplace = prog.memory[prog.instruction_pointer+num_param(typ)]
	prog.memory[oplace+1] = values[1] == values[2] ? 1 : 0
	prog.instruction_pointer += num_param(typ) + 1
end

struct AdjustRelativeBase <: Instruction 
	modes::Vector{Int}
end
num_param(typ::AdjustRelativeBase) = 1
function execute!(typ::AdjustRelativeBase, prog::Program) 
	values = read_values(prog, typ.modes)
	prog.relative_base += values[1]
	prog.instruction_pointer += num_param(typ) + 1
end

function split_opcode(code)
	opcode = code % 100
	modes =  Int[]
	for digit in 2:log10(code)
		push!(modes, code ÷ 10^digit % 10)
	end
	opcode, modes
end

function fill_modes!(typ::Instruction)
	num = num_param(typ)
	while length(typ.modes) < num 
		push!(typ.modes, 0)
	end
end

function get_instruction(code)
	opcode, modes = split_opcode(code)
	if opcode == 1
		op = Addition(modes)
		fill_modes!(op)
		return op
	elseif opcode == 2
		op = Multiplication(modes)
		fill_modes!(op)
		return op
	elseif opcode == 3
		return Input()
	elseif opcode == 4
		op = Output(modes)
		fill_modes!(op)
		return op
	elseif opcode == 5
		op = JumpIfTrue(modes)
		fill_modes!(op)
		return op
	elseif opcode == 6
		op = JumpIfFalse(modes)
		fill_modes!(op)
		return op
	elseif opcode == 7
		op = LessThan(modes)
		fill_modes!(op)
		return op
	elseif opcode == 8
		op = Equals(modes)
		fill_modes!(op)
		return op
	elseif opcode == 9
		op = AdjustRelativeBase(modes)
		fill_modes!(op)
		return op
	elseif opcode == 99
		return Halt()
	end
end