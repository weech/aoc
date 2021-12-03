module AOC.Day03

open System

type State = {Ones: int; Zeros: int}

let tallyUp (state, thisRound) = 

    match thisRound with
    | '1' -> {state with Ones = state.Ones + 1}
    | '0' -> {state with Zeros = state.Zeros + 1}
    | _ -> raise (ArgumentException "Unknown character")

let assembleBitString states = 
    states 
    |> Seq.map (fun ele -> if ele.Ones > ele.Zeros then '1' else '0')
    |> String.Concat

let statesToIndicators states = 
    let bitstring = assembleBitString states 
    let gamma = Convert.ToInt32(bitstring, 2)
    let inverted = bitstring |> Seq.map (fun c -> if c = '0' then '1' else '0') |> String.Concat
    let epsilon = Convert.ToInt32(inverted, 2)
    (gamma, epsilon)

let multiplyTuple (first, second) = first * second

let findBitCounts (data: string[]) = 
    let numColumns = String.length data[0]
    let startingStates = Array.create numColumns {Ones = 0; Zeros = 0}
    data 
    |> Seq.fold (fun states row -> row |> Seq.zip states |> Seq.map tallyUp) startingStates
    
let part1 (data: string[]) = 
    data 
    |> findBitCounts
    |> statesToIndicators 
    |> multiplyTuple

let rec testForRating comp column data = 
    if Array.length data = 1 then 
        data[0]
    else 
        let counts = data |> findBitCounts |> Array.ofSeq
        let winner = comp counts[column]
        data |> Array.filter (fun row -> row[column] = winner) |> testForRating comp (column + 1)

let part2 (data: string[]) = 
    let oxygen = testForRating (fun state -> if state.Ones >= state.Zeros then '1' else '0' ) 0 data
    let oxygen = Convert.ToInt32(oxygen, 2)
    let co2 = testForRating (fun state -> if state.Ones >= state.Zeros then '0' else '1' ) 0 data
    let co2 = Convert.ToInt32(co2, 2)
    oxygen * co2
    