module AOC.Day15 

let takeTurn (recitation, previous) turn = 
    if Map.containsKey previous recitation then 
        let lastSaid = Map.find previous recitation 
        Map.add previous (turn - 1) recitation, turn - lastSaid - 1 
    else
        Map.add previous (turn - 1) recitation, 0

let runGame data ending = 
    let recitation = Seq.zip data (seq {1 .. (List.length data) - 1}) |> Map.ofSeq
    seq {(List.length data) + 1 .. ending}
    |> Seq.fold takeTurn (recitation, List.last data)
    |> snd

let part1 data = 
    runGame data 2020

let part2 data = 
    runGame data 30000000