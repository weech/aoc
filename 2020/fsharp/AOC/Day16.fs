module AOC.Day16 
// Not complete
open System
open System.Text.RegularExpressions

let determineRanges rules = 
    Map (Seq.map (fun rule -> 
        let m = Regex.Match(rule, "(.+?)(?=:): (\d+)-(\d+) or (\d+)-(\d+)")
        let cap = Array.tail [| for g in m.Groups -> g.Value |]
        let name = cap.[1].Trim ()
        let range1 = seq { (int cap.[2]) .. (int cap.[3]) }
        let range2 = seq { (int cap.[4]) .. (int cap.[5]) }
        let vals = Set.ofSeq (Seq.append range1 range2)
        name, vals
    ) rules)

let values mapping = mapping |> Map.toSeq |> Seq.map (fun (_, value) -> value)

let testMatch rules item = Seq.exists (Set.contains item) (values rules)

let part1 data = 
    let lined = Array.map (fun (x: string) -> x.Split([|'\n'|])) data 
    let rules = determineRanges(lined.[0])
    Array.sumBy (fun (ticket: string) ->
                 (ticket.Split([|','|])
                 |> Seq.map int 
                 |> Seq.sumBy (fun entry -> if testMatch rules entry then 0 else entry)))
                 lined.[2].[1..]
                 
let identifyColumns data = 
    Map.empty

let part2 data = 
    0L