// using System.Text.Json;
// using PPModel;

// namespace PPDL
// {

//     public class Repository : IRepository
//     {


//         private string _filepath = "../PlanetPaintballDL/Database/";
//         private string _jsonString;


//         public Customer AddCustomer(Customer p_customer)
//         {

//             string path = _filepath + "PlanetPaintballCustomer.json";
            
//                 List<Customer> listOfCustomers = GetAllCustomers();
//                 listOfCustomers.Add(p_customer);
            
//                 _jsonString = JsonSerializer.Serialize(listOfCustomers, new JsonSerializerOptions {WriteIndented = true});

//                 File.WriteAllText(path, _jsonString);
            
//             return p_customer;

//         }

//         public List<Customer> GetAllCustomers()
//         {

//             _jsonString = File.ReadAllText(_filepath + "PlanetPaintballCustomer.json");

//             return JsonSerializer.Deserialize<List<Customer>>(_jsonString);

//         }

//         public Customer SearchCustomer(Customer p_customer)
//         {

//             List<Customer> listOfCustomers = GetAllCustomers();
//             return p_customer;

//         }


//         public List<StoreFront> GetStoreFronts()
//         {

//             _jsonString = File.ReadAllText(_filepath + "PlanetPaintballInventory.json");

//             return JsonSerializer.Deserialize<List<StoreFront>>(_jsonString);

//         }

//     }

// }