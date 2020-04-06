FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /MemberWorkflow
RUN mkdir -p nugetpackages
COPY . ./
WORKDIR  "/MemberWorkflow/Utilities"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/DatabaseEntities"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/BusinessObjects"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/AuthUtility"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/MemberDataAccess"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/MemberService"
RUN dotnet build
RUN dotnet pack -o /MemberWorkflow/nugetpackages
WORKDIR "/MemberWorkflow/MemberWorkFlow"
RUN dotnet build
RUN dotnet publish -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /MemberWorkflow
COPY --from=build-env /MemberWorkflow/MemberWorkFlow/Aliera.MemberWorkflow/out .
RUN export ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "Aliera.MemberWorkflow.dll"]
