module AOC.Day14

open System.Text.RegularExpressions
open System

type Rule = { Source: string; Insert: string }

let makeRule (line: string) =
    let m =
        Regex.Matches(line, @"([A-Z]{2}) -> ([A-Z])")
        |> Seq.exactlyOne

    let groups =
        m.Groups
        |> Seq.map (fun g -> g.Value)
        |> Seq.skip 1
        |> Seq.toArray

    { Source = groups.[0]
      Insert = groups.[1] }

let increaseOrInsert newCount oldCount =
    match oldCount with
    | Some x -> Some(x + newCount)
    | None -> Some newCount

let step chain (rules: Map<string, string * string>) =
    Map.fold
        (fun newChain pattern count ->
            if Map.containsKey pattern rules then
                let (p1, p2) = rules.[pattern]

                Map.change p1 (increaseOrInsert count) newChain
                |> Map.change p2 (increaseOrInsert count)
            else
                newChain)

        Map.empty
        chain

let getInitialCounts (chain: string) =
    Seq.windowed 2 chain
    |> Seq.map String.Concat
    |> Seq.countBy id
    |> Seq.map (fun (str, count) -> (str, uint64 count))
    |> Map.ofSeq

let makeRules ruleList =
    ruleList
    |> Seq.map (fun rule -> (rule.Source, (string rule.Source.[0] + rule.Insert, rule.Insert + string rule.Source.[1])))
    |> Map.ofSeq

let rec expandPoly rules maxCount chain count =
    if count > maxCount then
        chain
    else
        expandPoly rules maxCount (step chain rules) (count + 1)

let score startBit endBit chain =
    let decomposed =
        Map.fold
            (fun state (pattern: string) count ->
                let c1 = pattern.[0]
                let c2 = pattern.[1]

                Map.change c1 (increaseOrInsert count) state
                |> Map.change c2 (increaseOrInsert count))
            Map.empty
            chain
        |> Map.change endBit (increaseOrInsert 1uL)
        |> Map.change startBit (increaseOrInsert 1uL)

    let most = decomposed |> Map.values |> Seq.max
    let least = decomposed |> Map.values |> Seq.min
    ((most + 1uL) / 2uL) - ((least + 1uL) / 2uL)

let part1 seed ruleStrings =
    let rules =
        ruleStrings |> Seq.map makeRule |> makeRules

    let chain = getInitialCounts seed

    expandPoly rules 10 chain 1
    |> (score (Seq.head seed) (Seq.last seed))

let part2 seed ruleStrings =
    let rules =
        ruleStrings |> Seq.map makeRule |> makeRules

    let chain = getInitialCounts seed

    expandPoly rules 40 chain 1
    |> (score (Seq.head seed) (Seq.last seed))
