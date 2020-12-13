module Day13
import AOC.Parsers 

function part1(;data=Parsers.day13())
	start = data[1]
	buses = parse.(Int, filter(x -> x != "x", split(data[2], ',')))
	prod(minimum(map(id -> (id - (start % id), id), buses)))
end

function part2(;data=Parsers.day13())
	splat = split(data[2], ',')
	indexes = findall(x -> x != "x", splat)
	ns = parse.(Int, splat[indexes])
	# Chinese remainder theorem
	N = prod(ns)
	as = [n - (ind-1) for (n, ind) in zip(ns, indexes)]
	ys = [N ÷ n for n in ns]
	zs = [invmod(y, n) for (y, n) in zip(ys, ns)]
	sum([a * y * z for (a, y, z) in zip(as, ys, zs)]) % N
end

end