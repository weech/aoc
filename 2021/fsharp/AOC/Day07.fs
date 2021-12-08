module AOC.Day07

open System

let findMedian data =
    let sorted = Array.sort data
    let middleIndex = Array.length data / 2
    sorted.[middleIndex]

let part1 (data: int []) =
    let median = findMedian data

    data
    |> Seq.map (((-) median) >> Math.Abs)
    |> Seq.sum

// Extraneous but I wanted it
let findMean data =
    data
    |> Seq.sum
    |> float
    |> (fun x -> x / (data |> Array.length |> float))

let computeTriangular n = n * (n + 1) / 2

let computeCost data (guess: int) =
    data
    |> Seq.map ((-) guess >> Math.Abs >> computeTriangular)
    |> Seq.sum

// O(n^2) but whatever. It still computes in 26 ms
let loopy data =
    seq { Array.min data .. Array.max data }
    |> Seq.map (fun guess -> (guess, computeCost data guess))
    |> Seq.minBy snd

let part2 data =
    let result = loopy data
    //printfn "The middle position is %d" (fst result)
    //printfn "The mean is %f" (findMean data)
    // For whatever reason my example matches the rounded mean
    // but the actual input is the floored mean
    snd result
