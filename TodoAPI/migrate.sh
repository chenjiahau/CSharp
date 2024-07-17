comment=$1
context=$2
dotnet ef migrations add "$comment" -c "$context"
dotnet ef database update -c "$context"
