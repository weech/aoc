module AOC.Day4

open System
open System.Text.RegularExpressions

let groupEntry item = 
    Regex.Matches(item, "(\w+):(\S+)")
    |> Seq.cast<Match>
    |> Seq.map (fun mat -> (let cap = Array.tail [| for g in mat.Groups -> g.Value |]
                            (cap.[0], cap.[1])))
    |> Map 

let day4Inner validator data = 
    data
    |> Seq.filter (groupEntry >> validator)
    |> Seq.length

// 4.798 ms
let part1 data = 
    let validatePassport (passport: Map<string, string>) = 
        let mandatoryKeys = ["byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"]
        Seq.forall (fun key -> passport.ContainsKey(key)) mandatoryKeys
    day4Inner validatePassport data

// 5.717 ms
let part2 data = 
    let validatePassport (passport: Map<string, string>) = 
        passport.ContainsKey("byr") && Regex.IsMatch (passport.["byr"], "^19[2-9][0-9]$|^200[0-2]$") &&
        passport.ContainsKey("iyr") && Regex.IsMatch (passport.["iyr"], "^201[0-9]$|^2020$") && 
        passport.ContainsKey("eyr") && Regex.IsMatch (passport.["eyr"], "^202[0-9]$|^2030$") && 
        passport.ContainsKey("hgt") && Regex.IsMatch (passport.["hgt"], "^1[5-8][0-9]cm$|^19[0-3]cm$|^59in$|^6[0-9]in$|^7[0-6]in$") &&
        passport.ContainsKey("hcl") && Regex.IsMatch (passport.["hcl"], "^#[0-9a-f]{6}$") &&
        passport.ContainsKey("ecl") && Regex.IsMatch (passport.["ecl"], "^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$") &&
        passport.ContainsKey("pid") && Regex.IsMatch (passport.["pid"], "^\d{9}$")
    day4Inner validatePassport data