# Define PostgreSQL connection parameters
$PGServer = "" # Hostname or IP address of PostgreSQL server
$PGPort = ""# Port number of PostgreSQL server""
$PGDatabase = "" # Database name
$PGUser = "" # Username
$PGPassword = ""  # Store securely in production
$BatchSize = 25  # Number of rows per batch
$RetryDelay = 55  # Delay in seconds to avoid rate limits

# Function to count remaining NULL plot_vector records
function Get-NullCount {
    $CountQuery = "SELECT COUNT(*) FROM movies WHERE plot_vector IS NULL;"
    $Count = & psql -t -A -c "$CountQuery" "host=$PGServer port=$PGPort dbname=$PGDatabase user=$PGUser password=$PGPassword sslmode=require"
    return [int]$Count
}

# Loop until there are no more NULL plot_vector records
do {
    $RemainingNulls = Get-NullCount

    if ($RemainingNulls -gt 0) {
        Write-Host "$RemainingNulls records still need embeddings. Processing next batch..."

        # SQL statement to update plot_vector in batches
        $SQL = @"
        UPDATE movies
        SET plot_vector = azure_openai.create_embeddings('text-embedding-ada-002', plot)
        WHERE id IN (
            SELECT id FROM movies WHERE plot_vector IS NULL LIMIT $BatchSize
        );
"@

        # Execute the SQL command
        echo $SQL | psql "host=$PGServer port=$PGPort dbname=$PGDatabase user=$PGUser password=$PGPassword sslmode=require"

        # Wait before next batch to avoid OpenAI rate limits
        Write-Host "Waiting $RetryDelay seconds to prevent rate limits..."
        Start-Sleep -Seconds $RetryDelay
    }
} while ($RemainingNulls -gt 0)

Write-Host "All movies successfully updated with embeddings!"
