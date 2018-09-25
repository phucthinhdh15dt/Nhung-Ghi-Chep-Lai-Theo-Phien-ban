
các bước xây dựng asp.net core web api
 	bước 1: xây dựng database .
	bước 2: Tạo project web api .
	bước 3: Cấu hình conection string .
	bước 4: Tạo các Models ánh xạ database .
	bước 5: Tạo các lớp DbContext .
	bước 6: Tạo các class controller sữ dụng restful để code CRUD.

Các bước tính hợp Swagger vào project :
	bước 1 :Vào nuget package intall swashbuckle.AspNetCore 
	bước 2 :Đăng ký dịch vụ Swagger trong phần ConfigService 
	bước 3 :Config đường dẫn của swagger 
	bước 4 :chạy project theo dạng :protocol/Domain/swagger 