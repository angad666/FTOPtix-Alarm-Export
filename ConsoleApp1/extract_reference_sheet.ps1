$path = 'C:\Users\angad\OneDrive\Documents\FTOPtix Alarm Export\Alarms.xlsx'
 $tmpCsv = 'C:\Users\angad\OneDrive\Documents\FTOPtix Alarm Export\ConsoleApp1\Reference_Sheet_export.csv'
try {
    $xl = New-Object -ComObject Excel.Application
    $xl.Visible = $false
    $wb = $xl.Workbooks.Open($path, 0, $true)
    $ws = $wb.Worksheets.Item('Reference_Sheet')
    if (-not $ws) { Write-Error 'Sheet not found'; exit 1 }
    $used = $ws.UsedRange
    $rows = $used.Rows.Count
    $cols = $used.Columns.Count
    $lines = @()
    for ($r = 1; $r -le $rows; $r++) {
        $vals = @()
        for ($c = 1; $c -le $cols; $c++) {
            $cell = $used.Item($r, $c).Text
            if ($null -eq $cell) { $cell = '' }
            $vals += ($cell -replace '"', '""')
        }
        $lines += '"' + ($vals -join '","') + '"'
    }
    $wb.Close($false)
    $xl.Quit()
    $lines -join "`n" | Set-Content -Path $tmpCsv -Encoding UTF8
    Get-Content -Path $tmpCsv -Raw
} catch {
    Write-Error $_.Exception.Message
    exit 1
} finally {
    if ($xl) { $xl.Quit() }
}
