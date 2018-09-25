Specific type : Trả về kiểu dữ liệu nguyên thủy ,hay là một đối tượng .
EX: [HttpGet] public IEnumerable<Product> Get() { return _repository.GetProducts(); }

 IActionResult :khi chúng ta muốn trả về trạng thái HTTP như badrequest ,not found....

 Loại IActionResult :cho phép chúng ta trả về một json,View ...
 tham khảo :restfull api crud :https://www.codeproject.com/Articles/1203987/Creating-CRUD-API-in-ASP-NET-Core