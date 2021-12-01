module AOC.Day01

let part1 (data: int[]) = 
    Array.windowed 2 data
    |> Seq.map (fun window -> if window[1] > window[0] then 1 else 0)
    |> Seq.sum

let part2 (data: int[]) = 
    Array.windowed 3 data 
    |> Array.map Array.sum 
    |> part1