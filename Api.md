# CleanArichitectureWebApi API

- [CleanArichitectureWebApi API](#CleanArichitectureWebApi)
    - [Auth](#auth)
    	-[Register](#register)
            -[Register Request](#register-request)
			-[Register Responce](#register-responce)
		-[Login](#login)
    		-[Login Request](#login-request)
			-[Login Responce](#login-responce)

## Auth


### Register
```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
	"firstName":"George",
	"lastName":"Darchiashvili",
	"email":"George@Darchiashvili.com",
	"password":"George123"
}



### Login Response

'''json
{
	"email":"George@Darchiashvili.com",
	"password":"George123"
}