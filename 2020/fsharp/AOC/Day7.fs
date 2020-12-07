module AOC.Day7

open System 

type Rule = { Count: int; Color: string }

let parseBag (line: string) = 
    let words = line.Split([|' '|])
    let color = String.Join(" ", words.[0..1])
    let bagHalf = (String.Join(" ", words.[4..])).Split([|','|])
    match Array.head bagHalf with
    | "no other bags." -> (color, List.empty)
    | _ -> let bags = Seq.map (fun (rule: string) ->
                let rwords = rule.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
                let num = int rwords.[0]
                let rcolor = String.Join(" ", rwords.[1..2])
                {Count = num; Color = rcolor}) bagHalf
           (color, List.ofSeq bags)

let hasChild child (_, rules) = 
    List.exists (fun rule -> rule.Color = child) rules

let rec gatherParents bags desire = 
    let parentColors = bags
                    |> List.filter (hasChild desire)
                    |> List.map (fun (color, _) -> color)
    parentColors @ (parentColors 
                    |> List.map (gatherParents bags)
                    |> Seq.concat 
                    |> List.ofSeq)

let part1 data =
    let desire = "shiny gold"
    let bags = Seq.map parseBag data |> List.ofSeq
    gatherParents bags desire
    |> Set.ofList
    |> Set.count

let rec countChildren (bags: Map<string,list<Rule>>) desire = 
    bags.[desire]
    |> List.sumBy (fun rule -> rule.Count + (rule.Count * (countChildren bags rule.Color)))

let part2 data = 
    let desire = "shiny gold"
    let bags = Seq.map parseBag data |> Map
    countChildren bags desire