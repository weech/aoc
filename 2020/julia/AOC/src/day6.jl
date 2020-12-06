module Day6

make_set(group) = union(split.(group, "")...)
make_set2(group) = intersect(split.(group, "")...)

end

day6_part1(;data=Parsers.day6()) = sum(map(length ∘ Day6.make_set, data))

day6_part2(;data=Parsers.day6()) = sum(map(length ∘ Day6.make_set2, data))
