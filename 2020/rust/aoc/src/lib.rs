#![allow(dead_code)]
use std::collections::HashMap;
mod parsers;

fn day1_part1(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for right in left..data.len() {
            if data[left] + data[right] == 2020 {
                return data[left] * data[right];
            }
        }
    }
    0
}

fn day1_part2(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for center in left..data.len() {
            for right in center..data.len() {
                if data[left] + data[right] + data[center] == 2020 {
                    return data[left] * data[right] * data[center];
                }
            }
        }
    }
    0
}

fn extract_psk_and_rule(string: &str) -> (usize, usize, char, String) {
    let re = regex::Regex::new(r"(\d+)-(\d+) ([a-z]): (.+)").unwrap();
    let cap = re.captures(string).unwrap();
    (
        cap[1].parse().unwrap(),
        cap[2].parse().unwrap(),
        cap[3].chars().nth(0).unwrap(),
        cap[4].into(),
    )
}

fn day2_part1(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (min, max, letter, word) = extract_psk_and_rule(line);
            let letter_count = word.chars().filter(|x| *x == letter).count();
            letter_count >= min && letter_count <= max
        })
        .count()
}

fn day2_part2(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (first, last, letter, word) = extract_psk_and_rule(line);
            (word.chars().nth(first - 1).unwrap() == letter)
                != (word.chars().nth(last - 1).unwrap() == letter)
        })
        .count()
}

fn count_trees(slope: &(usize, usize), data: &[Vec<char>]) -> u64 {
    let mut pos = [0, 0];
    let (length, width) = (data.len(), data[0].len());
    let mut count = 0;
    while pos[0] < length {
        count += if data[pos[0]][pos[1] % width] == '#' {
            1
        } else {
            0
        };
        pos[0] += slope.0;
        pos[1] += slope.1;
    }
    count
}

fn day3_part1(data: &[Vec<char>]) -> u64 {
    count_trees(&(1, 3), data)
}

fn day3_part2(data: &[Vec<char>]) -> u64 {
    [(1usize, 1usize), (1, 3), (1, 5), (1, 7), (2, 1)]
        .iter()
        .fold(1, |accum, slope| accum * count_trees(slope, data))
}

fn group_entry(item: &str) -> HashMap<String, String> {
    let re = regex::Regex::new(r"(\w+):(\S+)").unwrap();
    let mut passport: HashMap<String, String> = HashMap::new();
    for cap in re.captures_iter(item) {
        passport.insert(cap[1].to_string(), cap[2].to_string());
    }
    passport
}

fn day4_inner(validator: fn(HashMap<String, String>) -> bool, data: &[String]) -> usize {
    data.iter()
        .filter(|item| validator(group_entry(item)))
        .count()
}

fn day4_part1(data: &[String]) -> usize {
    let validator = |passport: HashMap<String, String>| {
        let mandatory_keys = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
        mandatory_keys.iter().all(|key| passport.contains_key(*key))
    };
    day4_inner(validator, &data)
}

fn day4_part2(data: &[String]) -> usize {
    let validator = |passport: HashMap<String, String>| {
        let byr_re = regex::Regex::new(r"^19[2-9][0-9]$|^200[0-2]$").unwrap();
        let iyr_re = regex::Regex::new(r"^201[0-9]$|^2020$").unwrap();
        let eyr_re = regex::Regex::new(r"^202[0-9]$|^2030$").unwrap();
        let hgt_re =
            regex::Regex::new(r"^1[5-8][0-9]cm$|^19[0-3]cm$|^59in$|^6[0-9]in$|^7[0-6]in$").unwrap();
        let hcl_re = regex::Regex::new(r"^#[0-9a-f]{6}$").unwrap();
        let ecl_re = regex::Regex::new(r"^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$").unwrap();
        let pid_re = regex::Regex::new(r"^\d{9}$").unwrap();
        passport.contains_key("byr")
            && byr_re.is_match(&passport["byr"])
            && passport.contains_key("iyr")
            && iyr_re.is_match(&passport["iyr"])
            && passport.contains_key("eyr")
            && eyr_re.is_match(&passport["eyr"])
            && passport.contains_key("hgt")
            && hgt_re.is_match(&passport["hgt"])
            && passport.contains_key("hcl")
            && hcl_re.is_match(&passport["hcl"])
            && passport.contains_key("ecl")
            && ecl_re.is_match(&passport["ecl"])
            && passport.contains_key("pid")
            && pid_re.is_match(&passport["pid"])
    };
    day4_inner(validator, &data)
}

#[cfg(test)]
mod tests {
    #[test]
    fn day1_part1() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(514579, crate::day1_part1(&testdata));
        assert_eq!(842016, crate::day1_part1(&crate::parsers::day1()));
    }

    #[test]
    fn day1_part2() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(241861950, crate::day1_part2(&testdata));
        assert_eq!(9199664, crate::day1_part2(&crate::parsers::day1()));
    }

    #[test]
    fn day2_part1() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(2, crate::day2_part1(&testdata));
        assert_eq!(603, crate::day2_part1(&crate::parsers::day2()));
    }

    #[test]
    fn day2_part2() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(1, crate::day2_part2(&testdata));
        assert_eq!(404, crate::day2_part2(&crate::parsers::day2()));
    }

    #[test]
    fn day3_part1() {
        let testdata = vec![
            vec!['.', '.', '#', '#', '.', '.', '.', '.', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '.', '.', '.', '#', '.', '.'],
            vec!['.', '#', '.', '.', '.', '.', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#'],
            vec!['.', '#', '.', '.', '.', '#', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '#', '.', '.', '.', '.', '.'],
            vec!['.', '#', '.', '#', '.', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '#'],
            vec!['#', '.', '#', '#', '.', '.', '.', '#', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '#', '.', '.', '.', '#', '.', '#'],
        ];
        assert_eq!(7, crate::day3_part1(&testdata));
        assert_eq!(265, crate::day3_part1(&crate::parsers::day3()));
    }
    #[test]
    fn day3_part2() {
        let testdata = vec![
            vec!['.', '.', '#', '#', '.', '.', '.', '.', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '.', '.', '.', '#', '.', '.'],
            vec!['.', '#', '.', '.', '.', '.', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#'],
            vec!['.', '#', '.', '.', '.', '#', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '#', '.', '.', '.', '.', '.'],
            vec!['.', '#', '.', '#', '.', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '#'],
            vec!['#', '.', '#', '#', '.', '.', '.', '#', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '#', '.', '.', '.', '#', '.', '#'],
        ];
        assert_eq!(336, crate::day3_part2(&testdata));
        assert_eq!(3154761400, crate::day3_part2(&crate::parsers::day3()));
    }

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
        assert_eq!(2, crate::day4_part1(&testdata));
        assert_eq!(260, crate::day4_part1(&crate::parsers::day4()));
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
        assert_eq!(0, crate::day4_part2(&invalid));
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
        assert_eq!(4, crate::day4_part2(&valid));
        assert_eq!(153, crate::day4_part2(&crate::parsers::day4()));
    }
}
