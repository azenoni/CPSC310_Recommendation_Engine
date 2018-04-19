// Learn more about F# at http://fsharp.org
#r "C:\\Users\\Carlos\\.nuget\\packages\\fsharp.data\\2.4.6\\lib\\net45\\FSharp.Data.dll"
open FSharp.Data
open System

type schools_by_region = CsvProvider<"salaries-by-region.csv">
let data = schools_by_region.Load("salaries-by-region.csv")


for row in data.Rows do
  printfn "(schools-by-region) %A: %A, %A, %A, %A, %A, %A, %A " row.``School Name`` row.Region row.``Starting Median Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``


type salaries_by_college_type = CsvProvider<"salaries-by-college-type.csv">
let college_type_data = salaries_by_college_type.Load("salaries-by-college-type.csv")


for row in college_type_data.Rows do
    printfn "(salaries-by-college-type) %A: %A, %A, %A, %A, %A, %A, %A " row.``School Name`` row.``School Type`` row.``Starting Median Salary`` row.``Mid-Career Median Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``


type degrees_that_payback = CsvProvider<"degrees-that-pay-back.csv">
let degree_data  = degrees_that_payback.Load("degrees-that-pay-back.csv")

for row in degree_data.Rows do
    printfn "(degrees-that-pay-back) %A: %A, %A, %A, %A, %A, %A, %A" row.``Undergraduate Major`` row.``Starting Median Salary`` row.``Mid-Career Median Salary`` row.``Percent change from Starting to Mid-Career Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``


printfn "Input a region, major, and college type you would like in the following form: \n region,major,type"
let userInput = Console.ReadLine().Split ','

let return_regions c = data.Filter(fun row -> (String.Compare(row.Region, c, StringComparison.OrdinalIgnoreCase) = 0))
let return_majors c = degree_data.Filter(fun row -> (String.Compare(row.``Undergraduate Major``, c, StringComparison.OrdinalIgnoreCase) = 0))
let return_college_type c = college_type_data.Filter(fun row -> (String.Compare(row.``School Type``, c, StringComparison.OrdinalIgnoreCase) = 0))

for row in (return_regions userInput.[0]).Rows do
    printfn "%A" row.``School Name``

printfn "\n Following line is for major: %A" userInput.[1]

for row in (return_majors userInput.[1]).Rows do
    printfn "%A" row.``Starting Median Salary``

printfn "\n Following lines are regarding school type"

for row in (return_college_type userInput.[2]).Rows do
    printfn "%A" row.``School Name``