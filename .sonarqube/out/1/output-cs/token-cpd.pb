�
5D:\_lab\carllet-mvp\Domain\Entities\Budget\Earning.cs
	namespace 	
Domain
 
. 
Entities 
. 
Budget  
{ 
[
Table

(
 
$str
)
]
public 

class 
Earning 
{ 
[ 
Key
, 
DatabaseGenerated #
(# $#
DatabaseGeneratedOption$ ;
.; <
Identity< D
)D E
]E F
[ 
Column
( 
$str 
) 
]  
public 
Guid 
Id 
{ 
get  
;  !
set" %
;% &
}' (
[ 

ForeignKey
( 
$str 
) 
] 
[ 
Column
( 
$str '
)' (
]( )
public 
Guid 
? 
OwnerId  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 

JsonIgnore
] 
public 
User 
? 
Owner 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
Column
( 
$str *
)* +
]+ ,
public 
DateTime 
InsertionDateTime -
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
[ 
Column
( 
$str !
)! "
]" #
public 
double 
EarningValue &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
}!! 
}"" �
5D:\_lab\carllet-mvp\Domain\Entities\Budget\Expense.cs
	namespace 	
Domain
 
. 
Entities 
. 
Budget  
{ 
internal		 
class		
Expense		 
{

 
} 
} �
9D:\_lab\carllet-mvp\Domain\Entities\Budget\ExpenseType.cs
	namespace 	
Domain
 
. 
Entities 
. 
Budget  
{ 
internal		 
class		
ExpenseType		 
{

 
} 
} �
2D:\_lab\carllet-mvp\Domain\Entities\Budget\Goal.cs
	namespace 	
Domain
 
. 
Entities 
. 
Budget  
{ 
internal		 
class		
Goal		 
{

 
} 
} �
-D:\_lab\carllet-mvp\Domain\Entities\Course.cs
	namespace 	
Domain
 
. 
Entities 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Course 
{ 
[

 	
Key

	 
,

 
DatabaseGenerated

 
(

  #
DatabaseGeneratedOption

  7
.

7 8
Identity

8 @
)

@ A
]

A B
[ 	
Column	 
( 
$str 
) 
] 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
[ 	

ForeignKey	 
( 
$str 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
Guid 
? 
OwnerId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
User 
? 
Owner 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Column	 
( 
$str $
)$ %
]% &
public 
float 
CourseLength !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Column	 
( 
$str #
)# $
]$ %
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} �
+D:\_lab\carllet-mvp\Domain\Entities\User.cs
	namespace 	
Domain
 
. 
Entities 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
User 
{		 
[

 	
Key

	 
,

 
DatabaseGenerated

 
(

  #
DatabaseGeneratedOption

  7
.

7 8
Identity

8 @
)

@ A
]

A B
[ 	
Column	 
( 
$str 
) 
] 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
? 
Cnh 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Column	 
( 
$str 
) 
] 
[ 	

JsonIgnore	 
] 
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
? 
	Cellphone  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
? 
DeviceId 
{  !
get" %
;% &
set' *
;* +
}, -
[!! 	
Column!!	 
(!! 
$str!! 
)!!  
]!!  !
public"" 
string"" 
?"" 
RefreshToken"" #
{""$ %
get""& )
;"") *
set""+ .
;"". /
}""0 1
[$$ 	
Column$$	 
($$ 
$str$$ *
)$$* +
]$$+ ,
public%% 
DateTime%% 
?%% "
RefreshTokenExpiration%% /
{%%0 1
get%%2 5
;%%5 6
set%%7 :
;%%: ;
}%%< =
['' 	
Column''	 
('' 
$str'' 
)'' 
]''  
public(( 
string(( 
?(( 
AccessToken(( "
{((# $
get((% (
;((( )
set((* -
;((- .
}((/ 0
=((1 2
null((3 7
;((7 8
[** 	
Column**	 
(** 
$str** )
)**) *
]*** +
public++ 
DateTime++ 
?++ !
AccessTokenExpiration++ .
{++/ 0
get++1 4
;++4 5
set++6 9
;++9 :
}++; <
public-- 
List-- 
<-- 
Course-- 
>-- 
?-- 
Courses-- $
{--% &
get--' *
;--* +
set--, /
;--/ 0
}--1 2
}.. 
}// �
6D:\_lab\carllet-mvp\Domain\Entities\Vehicle\Vehicle.cs
	namespace 	
Domain
 
. 
Entities 
. 
Vehicle !
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Vehicle 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
, 
Order #
=$ %
$num& '
)' (
]( )
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public
int
Id
{
get
;
set
;
}
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
Brand 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
Model 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Column	 
( 
$str  
)  !
]! "
public 
short 
FabricationDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
Odometer 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Column	 
( 
$str 
) 
] 
public 
bool 
rented 
{ 
get  
;  !
set" %
;% &
}' (
}   
}!! �
;D:\_lab\carllet-mvp\Domain\Entities\Vehicle\VehicleBrand.cs
	namespace 	
Domain
 
. 
Entities 
. 
Vehicle !
{ 
internal		 
class		
VehicleBrand		 
{

 
} 
} �
:D:\_lab\carllet-mvp\Domain\Entities\Vehicle\VehicleType.cs
	namespace 	
Domain
 
. 
Entities 
. 
Vehicle !
{ 
internal		 
class		
VehicleType		 
{

 
} 
} �
5D:\_lab\carllet-mvp\Domain\Interfaces\IAuthService.cs
	namespace 	
Domain
 
. 

Interfaces 
{ 
public 

	interface 
IAuthService !
{ 
Task 
< 
string
> 
GenerateAccessToken (
(( )
User) -
user. 2
)2 3
;3 4
Task		 
<		 
string		
>		  
GenerateRefreshToken		 )
(		) *
User		* .
user		/ 3
)		3 4
;		4 5
Task

 
<

 
int


?

 
>

 


  
(

  !
string

! '
token

( -
)

- .
;

. /
} 
} �
7D:\_lab\carllet-mvp\Domain\Interfaces\ICourseService.cs
	namespace 	
Domain
 
. 

Interfaces 
{ 
public 

	interface 
ICourseService #
{ 
Task 
< 
Course
> 
Register 
( 
Course $
course% +
)+ ,
;, -
Task		 
<		 
List		
<		 
Course		 
>		 
>		 
GetByUserId		 &
(		& '
Guid		' +
driver		, 2
)		2 3
;		3 4
}

 
} �	
8D:\_lab\carllet-mvp\Domain\Interfaces\IEarningService.cs
	namespace 	
Domain
 
. 

Interfaces 
{ 
public 

	interface 
IEarningService $
{ 
Task		 
<		 
Earning		
>		 
RegisterEarning		 %
(		% &
Earning		& -
earning		. 5
)		5 6
;		6 7
Task

 
<

 
List


<

 
Earning

 
>

 
>

 
GetEarningByUser

 ,
(

, -
Guid

- 1
driver

2 8
)

8 9
;

9 :
Task 
< 
List
< 
Earning 
> 
> 
GetEarningByUser ,
(, -
Guid- 1
driver2 8
,8 9
DateTime: B
StartSearchC N
,N O
DateTimeP X
	EndSearchY b
)b c
;c d
Task 
< 
Earning
> 

(# $
Guid$ (
Id) +
)+ ,
;, -
}
} �

5D:\_lab\carllet-mvp\Domain\Interfaces\IUserService.cs
	namespace 	
Domain
 
. 

Interfaces 
{ 
public 

	interface 
IUserService !
{ 
Task 
< 
User
> 
Register 
( 
User  
user! %
)% &
;& '
Task 
< 
User
> 
GetUser 
( 
Guid 
id  "
)" #
;# $
Task		 
<		 
List		
<		 
User		 
>		 
>		 
GetUserList		 $
(		$ %
)		% &
;		& '
Task

 
<

 
User


>

 

DeleteUser

 
(

 
Guid

 "
id

# %
)

% &
;

& '
Task 
< 
User
> 
Login 
( 
string 
email  %
,% &
string' -
password. 6
)6 7
;7 8
Task 
< 
User
> 
Update 
( 
User 
user #
)# $
;$ %
}
} �	
8D:\_lab\carllet-mvp\Domain\Interfaces\IVehicleService.cs
	namespace 	
Domain
 
. 

Interfaces 
{ 
public 

	interface 
IVehicleService $
{ 
Task		 
<		 
Vehicle		
>		 

(		# $
Vehicle		$ +
vehicle		, 3
)		3 4
;		4 5
Task

 
<

 
Vehicle


>

 
GetVehicleById

 $
(

$ %
int

% (
id

) +
)

+ ,
;

, -
Task 
< 
List
< 
Vehicle 
> 
> 
GetVehicleByOwner -
(- .
Guid. 2
userId3 9
)9 :
;: ;
Task 
< 
List
< 
Vehicle 
> 
> 
GetVehicleList *
(* +
)+ ,
;, -
Task
<
Vehicle
>

(
int
id
)
;
} 
} 