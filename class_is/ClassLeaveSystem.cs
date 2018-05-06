using System;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IS.class_is
{
    public class ClassLeaveSystem : IHttpHandler
    {
        public DataSet getUserGroup() {
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            string sql;
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[USER_GROUP] ";
            ds = db.getData(sql);
            return ds;
        }

        public DataSet getUserRole()
        {
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            string sql;
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[USER_ROLE] ";
            ds = db.getData(sql);
            return ds;
        }

        public DataSet getDepartment()
        {
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            string sql;
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[DEPARTMENT] ";
            ds = db.getData(sql);
            return ds;
        }

        public DataSet getUserData(string userName) {
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            string sql;
            sql = "SELECT * ";
            sql += "FROM [LEAVE].[dbo].[USER] ";
            sql += "WHERE USER_NAME = '" + userName + "'";
            ds = db.getData(sql);
            return ds;
        }

        public string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            // If salt is not specified, generate it.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
            new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
            saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            context.Response.ContentType = "image/jpeg";
            response.BufferOutput = false;
            // get the key, the index into the DataTable
            int key = Convert.ToInt32(request.QueryString["Ind"]);
            // Prepare the datatable to hold the SNo key and the jpeg image, which will be written out 
            DataTable dt = new DataTable();
            dt = (DataTable)context.Session["dt"];
            if (!dt.Rows[key]["Evidence"].Equals(null))
            {
                byte[] imageOut = (byte[])dt.Rows[key]["Evidence"];
                response.OutputStream.Write(imageOut, 0, imageOut.Length);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
