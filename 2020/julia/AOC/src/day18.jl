module Day18
import AOC.Parsers

function tokenize(stream)
	tokens = [] 
	idx = 1
	while idx <= length(stream)
		if '0' <= stream[idx] <= '9' 
			# Go to next ) or ' ' 
			next_end_of_token = findnext(t -> t == ')' || t == ' ', stream, idx)
			next_end_index = isnothing(next_end_of_token) ? length(stream) : next_end_of_token - 1
			push!(tokens, parse(Int, stream[idx:next_end_index]))
			idx = next_end_index + 1
		elseif stream[idx] != ' '
			push!(tokens, stream[idx])
			idx += 1
		else 
			idx += 1
		end
	end
	tokens
end

operator_on_top(stack, ops) = length(stack) > 0 && (stack[end] ∈ keys(ops))

compare_precedence(sample, token, ops) = ops[sample][1] >= ops[token][1]

function evaluate_tokens(tokens, ops)
	output_queue = []
	operator_stack = []
	# Shunting-yard
	for token in tokens 
		if isa(token, Int)
			push!(output_queue, token)
		elseif token ∈ keys(ops)
			while operator_on_top(operator_stack, ops) && compare_precedence(operator_stack[end], token, ops)
				push!(output_queue, pop!(operator_stack))
			end
			push!(operator_stack, token)
		elseif token == '('
			push!(operator_stack, token)
		elseif token == ')' 
			while operator_stack[end] != '('
				push!(output_queue, pop!(operator_stack))
			end	
			pop!(operator_stack)
		end
	end
	append!(output_queue, reverse(operator_stack))

	values = []
	for token in output_queue
		if token ∉ keys(ops)
			push!(values, token)
		elseif token ∈ keys(ops)
			val1 = pop!(values)
			val2 = pop!(values)
			push!(values, ops[token][2](val2, val1))
		end
	end
	values[1]
end

evaluate(stream) = evaluate_tokens(tokenize(stream), Dict('*'=>(1, *), '+'=>(1, +)))

part1(;data=Parsers.day18()) = sum(evaluate, data)

evaluate2(stream) = evaluate_tokens(tokenize(stream), Dict('*'=>(1, *), '+'=>(2, +)))

part2(;data=Parsers.day18()) = sum(evaluate2, data)

end