member redeems points from an existing account: http://localhost/MemberManagementSystem/api/Accounts/RedeemPoints
POST
form-data [memberId, companyId, pointsCount]


member collects points to an existing account: http://localhost/MemberManagementSystem/api/Accounts/CollectPoints
POST
form-data [memberId, companyId, pointsCount]


user can export all members based on filter criteria: http://localhost/MemberManagementSystem/api/members/Export
Get
form-data [userId, minPointsCount, maxPointCount, accountStatus]


user can initially import existing members in a JSON format: http://localhost/MemberManagementSystem/api/Members/Import
POST
form-data [postedFile, UserId]


user creates a new account for a defined member: http://localhost/MemberManagementSystem/api/Accounts
POST
json
{
    "MemberId": 17,
    "Balance": 188,
    "Status": 1,
    "CompanyId": 5
}


user creates a new member: http://localhost/MemberManagementSystem/api/Users
POST
json
{
    "id": 2,
    "name": "Qais",
    "email": "qais-mail@email.com",
    "loginName": "loginN",
    "loginPassword": "loginP",
    "members": []
}
