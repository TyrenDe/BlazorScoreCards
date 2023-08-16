To deploy:
* Commit all pending changes to main
* dotnet publish .\BlazorScoreCards.csproj -c Release -o C:\temp\scorecard
* Open C:\temp\scorecard\wwwroot
* git checkout gh-pages
* Delete all except: .git .nojekyll .gitattribute
* Copy in all files from C:\temp\scorecard\wwwroot
* Edit index.html and swap out the "base" element
* Commit and push
* git checkout main
