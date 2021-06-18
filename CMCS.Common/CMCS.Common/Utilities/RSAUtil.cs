﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CMCS.Common.Utilities
{
	public class RSAUtil
	{
		/// <summary>
		/// RSA加密
		/// </summary>
		/// <param name="content">待加密字符</param>
		/// <param name="publickey">公钥</param>
		/// <returns></returns>
		public static string RSAEncrypt(string content, string publickey = "")
		{
			publickey = @"<RSAKeyValue><Modulus>MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCNFkzAQ21yrC1N8Bq1MrHTaL7Vu4ji23MnKLlD0TtGOQQWjLT293CelKwjYLqRBK5ZqwGD498NmxNBq/XJRtRaFCYXmXJLnruSpyVtL/cLVCZTLaaW/gf/cxYkUBg+Si1RGEPb2D22FVkfKKho94sQLKtw7i/ibN3kVvfjAGg/GQIDAQAB</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			byte[] cipherbytes;
			rsa.FromXmlString(publickey);
			cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

			return Convert.ToBase64String(cipherbytes);
		}

		/// <summary>
		/// RSA解密
		/// </summary>
		/// <param name="content">待解密字符</param>
		/// <param name="privatekey">私钥</param>
		/// <returns></returns>
		public static string RSADecrypt(string content, string privatekey = "")
		{
			privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			byte[] cipherbytes;
			rsa.FromXmlString(privatekey);
			cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

			return Encoding.UTF8.GetString(cipherbytes);
		}
	}
}