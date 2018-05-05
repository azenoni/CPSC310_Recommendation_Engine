//Kurt Lamon, Carlos Villagomez, Andrew Zenoni
//CPSC326 - Organization of Programming Languages
//May 5th, 2018
//v 1.0.0


//Open desired libraries
open FSharp.Data
open System

//Used to grab information from "salaries-by-region.csv"
type schools_by_region = CsvProvider<"salaries-by-region.csv">
let region_data = schools_by_region.Load("salaries-by-region.csv")

//Used to grab information from "salaries-by-college-type.csv"
type salaries_by_college_type = CsvProvider<"salaries-by-college-type.csv">
let college_type_data = salaries_by_college_type.Load("salaries-by-college-type.csv")

//Used to grab information from "degrees-that-pay-back.csv"
type degrees_that_payback = CsvProvider<"degrees-that-pay-back.csv">
let degree_data  = degrees_that_payback.Load("degrees-that-pay-back.csv")

//Based on the user input, goes through and returns all applicable schools in the "salaries-by-retion.csv" file
let return_regions userInput = 
    [for c in userInput do
        for x in region_data.Rows do
            if String.Compare(c, x.Region, StringComparison.OrdinalIgnoreCase) = 0 then
                yield x.``School Name``, x.Region, x.``Starting Median Salary``]

//Based on the user input, goes through and returns all applicable schools in the "degrees-that-pay-back.csv" file
let return_majors userInput = 
    [for c in userInput do
        for x in degree_data.Rows do
            if String.Compare(c, x.``Undergraduate Major``, StringComparison.OrdinalIgnoreCase) = 0 then
                yield x.``Undergraduate Major``, x.``Starting Median Salary``]

//Based on the user input, goes through and returns all applicable schools in the "salaries-by-college-type.csv" file
let return_college_type userInput = 
    [for c in userInput do
        for x in college_type_data.Rows do
            if String.Compare(c, x.``School Type``, StringComparison.OrdinalIgnoreCase) = 0 then
                yield x.``School Name``, x.``School Type``, x.``Starting Median Salary``]

//Performs comparisons based on the information that the user has inputted
let comparison (regions : (String*String*Decimal) List) (majors : (String*Decimal) List) (types : (String*String*Decimal) List) = 
    [ for region_name,region,region_salary in regions do
        for major,major_salary in majors do
            for type_name,single_type,_ in types do
                if (String.Compare(region_name, type_name, StringComparison.OrdinalIgnoreCase) = 0) && (Decimal.Compare(region_salary, major_salary) > 0) then
                    yield region_name, region, single_type, major]

[<EntryPoint>]
let main _ = 

    printfn "Please input the regions from the list below you would like to go to school in (seperated by commas, no spaces):\n California, Western, Midwestern, Southern, Northeastern \n"
    let userDefinedRegions = return_regions (Console.ReadLine().Split ',')

    printfn "\nPlease input the majors from the list below you would like to possible major in (seperated by commas, no spaces):\n Accounting, Aerospace Engineering, Agriculture, Anthropology, Architecture, Art History, Biology, Business Management, Chemical Engineering, Chemistry, Civil Engineering, Communications, Computer Engineering, Computer Science, Construction, Criminal Justice, Drama, Economics, Education, Electrical Engineering, English, Film, Finance, Forestry, Geography, Geology, Graphic Design, Health Care Administration, History, Hospitality & Tourism, Industrial Engineering, Information Technology (IT), Interior Design, International Relations, Journalism, Management Information Systems (MIS), Marketing, Math, Mechanical Engineering, Music, Nursing, Nutrition, Philosphy, Physician Assistant, Physics, Political Science, Psychology, Religion, Sociology, Spanish\n"
    let userDefinedMajors = return_majors (Console.ReadLine().Split ',')

    printfn "\nPlease input the types of schools you are interested in from the list below (seperate by commas, no spaces):\n Engineering, Party, Liberal Arts, Ivy League, State\n"
    let userDefinedTypes = return_college_type (Console.ReadLine().Split ',')

    printfn "Press enter to see your filtered results:"
    Console.ReadLine()
    for school in comparison userDefinedRegions userDefinedMajors userDefinedTypes do
        printfn "%A" school

    Console.ReadLine()
    
    0 // return an integer exit code
