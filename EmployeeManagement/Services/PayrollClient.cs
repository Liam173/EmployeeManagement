public class PayrollClient
{
    private readonly HttpClient _httpClient;

    public PayrollClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task NotifyEmployeeCreatedAsync(int employeeId)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/employees",
            new
            {
                EmployeeId = employeeId
            });

        response.EnsureSuccessStatusCode();
    }
}