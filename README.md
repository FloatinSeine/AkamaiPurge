AkamaiPurge
===========

A small console app to flush media on the Akamai CDN

Command Line Options
<pre>
-d = [production | staging] Domain Type
-t = [cp | arl]		          Purge Type 
-a = [invalidate | remove]  Action Type
-e = [email address]	      Email to notify when complete
-f = [filepath]		          Path to file containing ARLs to Purge. New line for each ARL
-?			                    Help
</pre>

Example<br/>
console.exe -d=production -t=arl -a=invalidate -e="recipient@company.com" -f="c:\\path\\arls.txt"<br/>
console.exe -?<br/>

API Result codes follow this pattern (from the CCUAPI docs):<br/>
1xx - Successful Request<br/>
2xx - Warning; reserved. The removal request has been accepted.<br/>
3xx - Bad or invalid request.<br/>
4xx - Contact Akamai Customer Care.<br/>
