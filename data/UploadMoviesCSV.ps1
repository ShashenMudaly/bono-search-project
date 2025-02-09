# Define database connection parameters
$PGServer = "" # Hostname or IP address of PostgreSQL server
$PGPort = "" # Port number of PostgreSQL server
$PGDatabase = "" # Database name
$PGUser = "" # Username
$PGPassword = ""  # Store securely in production
$CSVFilePath = "movie_list_small.csv"  # Update with the correct relative path

# Read the CSV file (skip the header row)
$Movies = Import-Csv -Path $CSVFilePath

# Iterate through each row in the CSV and insert into PostgreSQL
foreach ($Movie in $Movies) {
    $Title = $Movie.title -replace "'", "''"  # Escape single quotes
    $PlotSynopsis = $Movie.plot_synopsis -replace "'", "''"  # Escape single quotes

    # Construct SQL INSERT statement
    $SQL = @"
    INSERT INTO movies (name, plot)
    VALUES ('$Title', '$PlotSynopsis');
"@

    # Execute SQL command using psql
    Write-Host "Inserting: $Title"
    echo $SQL | psql "host=$PGServer port=$PGPort dbname=$PGDatabase user=$PGUser password=$PGPassword sslmode=require"
}

Write-Host "All movies inserted successfully!"
