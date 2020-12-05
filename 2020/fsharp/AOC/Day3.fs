module AOC.Day3

open System

let countTrees (slopeX: int, slopeY: int) (data: int [,]) = 
    let xs = Seq.toArray (seq {for n in 0 .. slopeX .. (Array2D.length1 data - 1) do n})
    let ys = seq {for n in 0 .. (Array.length xs - 1) do (n * slopeY) % Array2D.length2 data}
    Seq.zip xs ys
    |> Seq.sumBy (fun (x, y) -> data.[x, y])

    
let part1 data = 
    int64 (countTrees (1, 3) data)

let part2 data = 
    Seq.fold (fun accum slope -> accum * int64 (countTrees slope data)) 
        1L
        [ (1, 1); (1, 3); (1, 5); (1, 7); (2, 1) ]
