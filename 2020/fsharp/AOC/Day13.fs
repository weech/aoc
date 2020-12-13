module AOC.Day13


let part1 (start, (schedule: string)) = 
    let (diff, bus) = schedule.Split([|','|])
                        |> Seq.filter ((<>) "x")
                        |> Seq.map (int >> (fun bus -> (bus- (start % bus), bus)))
                        |> Seq.minBy (fun (diff, _) -> diff)
    diff * bus

let sieve pairs = 
    let rec catchUp res step off div = 
        if ((res + off) % div) = 0L then res
        else catchUp (res + step) step off div
    let (foff, fdiv) = List.head pairs
    pairs 
    |> Seq.fold (fun (res, step) (off, div) -> ((catchUp res step off div), step * div)) (fdiv-foff, 1L)
    |> fst

let part2 (_, schedule: string) = 
    let pairs = schedule.Split([|','|])
                |> Seq.mapi (fun idx ele -> (idx, ele))
                |> Seq.choose (fun (idx, ele) -> if ele = "x" then None 
                                                    else Some (int64 idx, int64 ele))
                |> List.ofSeq
                |> List.sortByDescending snd
    sieve pairs
                                                    
