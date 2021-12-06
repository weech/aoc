module AOC.Day06

let evaluateFish (cohorts: uint64[]) =
    // Hardcoding the array to avoid mutation yolo
    [|
        cohorts[1]
        cohorts[2]
        cohorts[3]
        cohorts[4]
        cohorts[5]
        cohorts[6]
        cohorts[7] + cohorts[0]
        cohorts[8]
        cohorts[0]
    |]

let rec cycle iteration ending fishes =
    if iteration >= ending then
        fishes
    else
        cycle (iteration + 1) ending (evaluateFish fishes) 

let decompose (fish: uint64 list) = 
    [|
        fish |> Seq.sumBy (fun x -> if x = 0UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 1UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 2UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 3UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 4UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 5UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 6UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 7UL then 1 else 0) |> uint64
        fish |> Seq.sumBy (fun x -> if x = 8UL then 1 else 0) |> uint64
    |]

let part1 data = data |> decompose |> cycle 0 80 |> Array.sum

let part2 data = data |> decompose |> cycle 0 256 |> Array.sum
