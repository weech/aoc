using Test 
using AOC2019

@testset "AOC2019" begin 

	# Day 9
	quinedata = [109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99]
	output = Channel{Int}(32)
	program = AOC2019.Program(quinedata, nothing, output)
	AOC2019.execute_program!(program)
	outdata = []
	while isready(output)
		push!(outdata, take!(output))
	end
	println(outdata)
	@test all(quinedata .== outdata)

	data16 = [1102,34915192,34915192,7,4,7,99,0]
	program = AOC2019.Program(data16, nothing, output)
	AOC2019.execute_program!(program)
	@test length(string(take!(output))) == 16

	databig = [104,1125899906842624,99]
	program = AOC2019.Program(databig, nothing, output)
	AOC2019.execute_program!(program)
	@test take!(output) == 1125899906842624

end