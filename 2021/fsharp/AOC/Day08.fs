module AOC.Day08

open System

let part1 data =
    data
    |> Seq.map (fun (row: string) ->
        row.Split('|').[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun item ->
            if Seq.exists ((=) (String.length item)) [ 2; 4; 3; 7 ] then
                1
            else
                0)
        |> Seq.sum)
    |> Seq.sum

// Hacking it out. It's not pretty, but it worked on the first try
let solveDigit (one, four, seven, eight, l5, l6) =
    let bottom =
        l6
        |> Seq.map (fun item -> item - (four + seven))
        |> Seq.find (Set.count >> (=) 1)

    let nine = four + seven + bottom

    let topLTopR =
        l5
        |> Seq.map (Set.difference nine)
        |> Seq.filter (Set.count >> (=) 1)

    let bottomRight =
        topLTopR
        |> Seq.map (fun item -> one - item)
        |> Seq.find (Set.count >> (=) 1)

    let topRight = one - bottomRight
    let five = nine - topRight
    let six = eight - topRight

    let zero =
        l6
        |> Seq.filter (fun item -> not (item = nine || item = six))
        |> Seq.exactlyOne

    let middle = eight - zero
    let three = bottom + middle + seven

    let two =
        l5
        |> Seq.filter (fun item -> not (item = three || item = five))
        |> Seq.exactlyOne

    [| zero
       one
       two
       three
       four
       five
       six
       seven
       eight
       nine |]

let setifyString (rowPiece: string) =
    rowPiece.Split(' ', StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map Set.ofSeq

let separateCounts sets =
    let one =
        sets
        |> Seq.filter (Set.count >> (=) 2)
        |> Seq.exactlyOne

    let four =
        sets
        |> Seq.filter (Set.count >> (=) 4)
        |> Seq.exactlyOne

    let seven =
        sets
        |> Seq.filter (Set.count >> (=) 3)
        |> Seq.exactlyOne

    let eight =
        sets
        |> Seq.filter (Set.count >> (=) 7)
        |> Seq.exactlyOne

    let l5 = sets |> Seq.filter (Set.count >> (=) 5)
    let l6 = sets |> Seq.filter (Set.count >> (=) 6)
    (one, four, seven, eight, l5, l6)

let realizeDigit digits jumble = digits |> Seq.findIndex ((=) jumble)

let part2 data =
    data
    |> Seq.map (fun (row: string) ->
        let pieces = row.Split('|')

        let solvedDigits =
            pieces.[0]
            |> setifyString
            |> separateCounts
            |> solveDigit

        let realDigits =
            pieces.[1]
            |> setifyString
            |> Seq.map (realizeDigit solvedDigits)
            |> Array.ofSeq

        realDigits.[0] * 1000
        + realDigits.[1] * 100
        + realDigits.[2] * 10
        + realDigits.[3])
    |> Seq.sum
