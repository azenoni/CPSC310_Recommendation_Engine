// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open FSharp.Data
open System

printfn "The program will pause throughout execution, press enter to continue"
printfn "Input a region, major, and college type you would like in the following form: \n region,major,type"

type schools_by_region = CsvProvider<"salaries-by-region.csv">
let data = schools_by_region.Load("salaries-by-region.csv")

type salaries_by_college_type = CsvProvider<"salaries-by-college-type.csv">
let college_type_data = salaries_by_college_type.Load("salaries-by-college-type.csv")

type degrees_that_payback = CsvProvider<"degrees-that-pay-back.csv">
let degree_data  = degrees_that_payback.Load("degrees-that-pay-back.csv")

let userInput = Console.ReadLine().Split ','

let return_regions c = data.Filter(fun row -> (String.Compare(row.Region, c, StringComparison.OrdinalIgnoreCase) = 0))
let return_majors c = degree_data.Filter(fun row -> (String.Compare(row.``Undergraduate Major``, c, StringComparison.OrdinalIgnoreCase) = 0)).Rows |> Seq.head
let return_college_type c = college_type_data.Filter(fun row -> (String.Compare(row.``School Type``, c, StringComparison.OrdinalIgnoreCase) = 0))

let calculate_major_benchmark = 
    if ((return_majors userInput.[1]).``Starting Median Salary``) > 55000M then ((return_majors userInput.[1]).``Starting Median Salary`` - 7000M)
    else (return_majors userInput.[1]).``Starting Median Salary``

let combine_college_region_and_major = (return_regions userInput.[0]).Filter(fun row -> (Decimal.Compare(row.``Starting Median Salary``, calculate_major_benchmark) > 0))

//Filters on first row, but not others
//let run_through_college_type = 
//    (return_college_type userInput.[2]).Filter(fun row -> (String.Compare(row.``School Name``, (combine_college_region_and_major.Rows |> Seq.head).``School Name``) = 0))

//Implemented in last section of main argv...
//let comparison = 
//    for row in combine_college_region_and_major.Rows do
//        for otherRow in (return_college_type userInput.[2]).Rows do
//            if (row.``School Name`` = otherRow.``School Name``) then
//                printfn "%A" row.``School Name``


[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    for row in data.Rows do
        printfn "(schools-by-region) %A: %A, %A, %A, %A, %A, %A, %A " row.``School Name`` row.Region row.``Starting Median Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``

    Console.ReadLine()
    for row in college_type_data.Rows do
        printfn "(salaries-by-college-type) %A: %A, %A, %A, %A, %A, %A, %A " row.``School Name`` row.``School Type`` row.``Starting Median Salary`` row.``Mid-Career Median Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``

    Console.ReadLine()
    for row in degree_data.Rows do
        printfn "(degrees-that-pay-back) %A: %A, %A, %A, %A, %A, %A, %A" row.``Undergraduate Major`` row.``Starting Median Salary`` row.``Mid-Career Median Salary`` row.``Percent change from Starting to Mid-Career Salary`` row.``Mid-Career 10th Percentile Salary`` row.``Mid-Career 25th Percentile Salary`` row.``Mid-Career 75th Percentile Salary`` row.``Mid-Career 90th Percentile Salary``

    Console.ReadLine()
    for row in (return_regions userInput.[0]).Rows do
        printfn "%A" row.``School Name``

    printfn "\n Following line is for major: %A" userInput.[1]

    Console.ReadLine()
    printfn "%A" (return_majors userInput.[1]).``Starting Median Salary``

    printfn "\n Following lines are regarding school type"

    Console.ReadLine()
    for row in (return_college_type userInput.[2]).Rows do
        printfn "%A" row.``School Name``

    printfn ""
    printfn "Your resutls for a %A school with starting median salary %A are:" userInput.[0] calculate_major_benchmark
    Console.ReadLine()
    for row in (combine_college_region_and_major).Rows do
        printfn "%A" row.``School Name``


    Console.ReadLine()

    printfn "Final results are: "
    for row in combine_college_region_and_major.Rows do
        for otherRow in (return_college_type userInput.[2]).Rows do
            if (row.``School Name`` = otherRow.``School Name``) then
                printfn "%A with a median starting salary of %A in the %A region" row.``School Name`` row.``Starting Median Salary`` row.Region
    Console.ReadLine()
    
    0 // return an integer exit code
