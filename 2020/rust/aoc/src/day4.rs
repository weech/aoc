use regex::Regex;
use std::collections::HashMap;

fn group_entry(item: &str) -> HashMap<String, String> {
    lazy_static! {
        static ref RE: Regex = Regex::new(r"(\w+):(\S+)").unwrap();
    }
    let mut passport: HashMap<String, String> = HashMap::new();
    for cap in RE.captures_iter(item) {
        passport.insert(cap[1].to_string(), cap[2].to_string());
    }
    passport
}

fn inner(validator: fn(HashMap<String, String>) -> bool, data: &[String]) -> usize {
    data.iter()
        .filter(|item| validator(group_entry(item)))
        .count()
}

pub fn part1(data: &[String]) -> usize {
    let validator = |passport: HashMap<String, String>| {
        let mandatory_keys = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
        mandatory_keys.iter().all(|key| passport.contains_key(*key))
    };
    inner(validator, &data)
}

// Currently > 1 ms
pub fn part2(data: &[String]) -> usize {
    let validator = |passport: HashMap<String, String>| {
        lazy_static! {
            static ref BYR_RE: Regex = Regex::new(r"^19[2-9][0-9]$|^200[0-2]$").unwrap();
        }
        lazy_static! {
            static ref IYR_RE: Regex = Regex::new(r"^201[0-9]$|^2020$").unwrap();
        }
        lazy_static! {
            static ref EYR_RE: Regex = Regex::new(r"^202[0-9]$|^2030$").unwrap();
        }
        lazy_static! {
            static ref HGT_RE: Regex =
                Regex::new(r"^1[5-8][0-9]cm$|^19[0-3]cm$|^59in$|^6[0-9]in$|^7[0-6]in$").unwrap();
        }
        lazy_static! {
            static ref HCL_RE: Regex = Regex::new(r"^#[0-9a-f]{6}$").unwrap();
        }
        lazy_static! {
            static ref ECL_RE: Regex =
                Regex::new(r"^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$").unwrap();
        }
        lazy_static! {
            static ref PID_RE: Regex = Regex::new(r"^\d{9}$").unwrap();
        }
        passport.contains_key("byr")
            && BYR_RE.is_match(&passport["byr"])
            && passport.contains_key("iyr")
            && IYR_RE.is_match(&passport["iyr"])
            && passport.contains_key("eyr")
            && EYR_RE.is_match(&passport["eyr"])
            && passport.contains_key("hgt")
            && HGT_RE.is_match(&passport["hgt"])
            && passport.contains_key("hcl")
            && HCL_RE.is_match(&passport["hcl"])
            && passport.contains_key("ecl")
            && ECL_RE.is_match(&passport["ecl"])
            && passport.contains_key("pid")
            && PID_RE.is_match(&passport["pid"])
    };
    inner(validator, &data)
}

#[cfg(test)]
mod tests {

    #[test]
    fn day4_part1() {
        let testdata = [
            "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
        byr:1937 iyr:2017 cid:147 hgt:183cm"
                .to_string(),
            "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
        hcl:#cfa07d byr:1929"
                .to_string(),
            "hcl:#ae17e1 iyr:2013
        eyr:2024
        ecl:brn pid:760753108 byr:1931
        hgt:179cm"
                .to_string(),
            "hcl:#cfa07d eyr:2025 pid:166559648
        iyr:2011 ecl:brn hgt:59in"
                .to_string(),
        ];
        assert_eq!(2, crate::day4::part1(&testdata));
        assert_eq!(260, crate::day4::part1(&crate::parsers::day4()));
    }

    #[test]
    fn day4_part2() {
        let invalid = [
            "eyr:1972 cid:100
        hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926"
                .to_string(),
            "iyr:2019
        hcl:#602927 eyr:1967 hgt:170cm
        ecl:grn pid:012533040 byr:1946"
                .to_string(),
            "hcl:dab227 iyr:2012
        ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277"
                .to_string(),
            "hgt:59cm ecl:zzz
        eyr:2038 hcl:74454a iyr:2023
        pid:3556412378 byr:2007"
                .to_string(),
        ];
        assert_eq!(0, crate::day4::part2(&invalid));
        let valid = [
            "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
        hcl:#623a2f"
                .to_string(),
            "eyr:2029 ecl:blu cid:129 byr:1989
        iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm"
                .to_string(),
            "hcl:#888785
        hgt:164cm byr:2001 iyr:2015 cid:88
        pid:545766238 ecl:hzl
        eyr:2022"
                .to_string(),
            "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719".to_string(),
        ];
        assert_eq!(4, crate::day4::part2(&valid));
        assert_eq!(153, crate::day4::part2(&crate::parsers::day4()));
    }
}
