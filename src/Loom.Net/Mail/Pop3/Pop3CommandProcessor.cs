#region Using Directives

using System;

#endregion

namespace Loom.Net.Mail.Pop3
{
    /// <summary>
    /// </summary>
    public class Pop3CommandProcessor
    {
        private readonly Pop3SessionProcessor sessionProcessor;

        private CommandState state;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="Pop3CommandProcessor" /> class.
        /// </summary>
        /// <param name="sessionProcessor">
        ///     The session processor that is hosting this
        ///     command processor.
        /// </param>
        public Pop3CommandProcessor(Pop3SessionProcessor sessionProcessor)
        {
            this.sessionProcessor = sessionProcessor;
        }

        /// <summary>
        ///     Gets or sets the username a client is using to authenticate.
        /// </summary>
        /// <remarks>
        ///     The processor needs to cache the username until the PASS command
        ///     is given. Once the PASS command is given, the processor may begin the
        ///     authentication process.
        /// </remarks>
        /// <value>The username a client is using to authenticate.</value>
        internal string UserName { get; set; }

        /// <summary>
        ///     Occurs when the client is attempting to authenticate, but before
        ///     the authentication is completed.
        /// </summary>
        public event EventHandler<UserAuthenticatingEventArgs> UserAuthenticating;

        /// <summary>
        ///     Occurs once the client has successfully authenticated.
        /// </summary>
        public event EventHandler<UserAuthenticatedEventArgs> UserAuthenticated;

        /// <summary>
        ///     Processes the specified command.
        /// </summary>
        /// <param name="command">The command sent by the client.</param>
        public string Process(string command)
        {
            return Process(command, new string[0]);
        }

        /// <summary>
        ///     Processes the specified command using the specified arguments.
        /// </summary>
        /// <param name="command">The command sent by the client.</param>
        /// <param name="args">The arguments received with the command.</param>
        public string Process(string command, params string[] args)
        {
            UpdateState(command);
            return state.Process(args);
        }

        private void UpdateState(string command)
        {
            switch (command)
            {
                case Commands.USER:
                    state = new UserState();
                    break;
                case Commands.PASS:
                    state = new PassState();
                    break;
                case Commands.STAT:
                    state = new StatState();
                    break;
                case Commands.LIST:
                    state = new ListState();
                    break;
                case Commands.RETR:
                    state = new RetrState();
                    break;
                case Commands.DELE:
                    state = new DeleState();
                    break;
                case Commands.NOOP:
                    state = new NoopState();
                    break;
                case Commands.RSET:
                    state = new RestState();
                    break;
                case Commands.QUIT:
                    state = new QuitState();
                    break;
                case Commands.UIDL:
                    state = new UildState();
                    break;
                case Commands.APOP:
                    state = new ApopState();
                    break;
                case Commands.TOP:
                    state = new TopState();
                    break;
                case Commands.AUTH:
                    state = new AuthState();
                    break;
                case Commands.CAPA:
                    state = new CapaState();
                    break;
                case Commands.STLS:
                    state = new StlsState();
                    break;
            }
            state.SessionProcessor = sessionProcessor;
            state.CommandProcessor = this;
        }

        /// <summary>
        ///     Raises the <see cref="Pop3CommandProcessor.UserAuthenticating" />
        ///     event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="UserAuthenticatingEventArgs" />
        ///     instance containing the event data.
        /// </param>
        /// <remarks>
        ///     Occurs when the client is attempting to authenticate, but before
        ///     the authentication is completed.
        /// </remarks>
        protected virtual void OnUserAuthenticating(UserAuthenticatingEventArgs e)
        {
            EventHandler<UserAuthenticatingEventArgs> handler = UserAuthenticating;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Pop3CommandProcessor.UserAuthenticated" />
        ///     event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="UserAuthenticatedEventArgs" />
        ///     instance containing the event data.
        /// </param>
        /// <remarks>Occurs once the client has successfully authenticated.</remarks>
        protected virtual void OnUserAuthenticated(UserAuthenticatedEventArgs e)
        {
            EventHandler<UserAuthenticatedEventArgs> handler = UserAuthenticated;
            if (handler != null)
                handler(this, e);
        }

        #region Nested type: ApopState

        /// <summary>
        ///     Represents the Pop3 protocol APOP command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the APOP command.
        ///     <code>
        /// RFC 1939.7 APOP
        /// 	Arguments:
        /// 		a string identifying a mailbox and a MD5 digest string
        /// 		(both required)
        /// 		
        /// 	Note:
        /// 		A POP3 server which implements the APOP command will
        /// 		include a timestamp in its banner greeting.  The syntax of
        /// 		the timestamp corresponds to the `msg-id' in [RFC822], and
        /// 		MUST be different each time the POP3 server issues a banner
        /// 		greeting.
        /// 		
        /// 	Examples:
        /// 		S: +OK POP3 server ready <![CDATA[<1896.697170952@dbc.mtview.ca.us>]]>
        /// 		C: APOP mrose c4c9334bac560ecc979e58001b3e22fb
        /// 		S: +OK maildrop has 1 message (369 octets)
        /// 
        /// 		In this example, the shared  secret  is  the  string  `tan-
        /// 		staaf'.  Hence, the MD5 algorithm is applied to the string
        /// 
        /// 		<![CDATA[<1896.697170952@dbc.mtview.ca.us>]]>tanstaaf
        /// 		 
        /// 		which produces a digest value of c4c9334bac560ecc979e58001b3e22fb
        /// </code>
        /// </remarks>
        private class ApopState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: AuthState

        /// <summary>
        ///     Represents the Pop3 protocol AUTH command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the AUTH command.
        ///     <code>
        /// RFC 1734 AUTH
        /// 	Arguments:
        /// 		a string identifying an IMAP4 authentication mechanism,
        /// 		such as defined by [IMAP4-AUTH].  Any use of the string
        /// 		"imap" used in a server authentication identity in the
        /// 		definition of an authentication mechanism is replaced with
        /// 		the string "pop".
        /// 				
        /// 	Possible Responses:
        /// 		+OK maildrop locked and ready
        /// 		-ERR authentication exchange failed
        /// 
        /// 	Restrictions:
        /// 		may only be given in the AUTHORIZATION state
        /// 
        /// 	Discussion:
        /// 		The AUTH command indicates an authentication mechanism to
        /// 		the server.  If the server supports the requested
        /// 		authentication mechanism, it performs an authentication
        /// 		protocol exchange to authenticate and identify the user.
        /// 		Optionally, it also negotiates a protection mechanism for
        /// 		subsequent protocol interactions.  If the requested
        /// 		authentication mechanism is not supported, the server						
        /// 		should reject the AUTH command by sending a negative
        /// 		response.
        /// 
        /// 		The authentication protocol exchange consists of a series
        /// 		of server challenges and client answers that are specific
        /// 		to the authentication mechanism.  A server challenge,
        /// 		otherwise known as a ready response, is a line consisting
        /// 		of a "+" character followed by a single space and a BASE64
        /// 		encoded string.  The client answer consists of a line
        /// 		containing a BASE64 encoded string.  If the client wishes
        /// 		to cancel an authentication exchange, it should issue a
        /// 		line with a single "*".  If the server receives such an
        /// 		answer, it must reject the AUTH command by sending a
        /// 		negative response.
        /// 
        /// 		A protection mechanism provides integrity and privacy
        /// 		protection to the protocol session.  If a protection
        /// 		mechanism is negotiated, it is applied to all subsequent
        /// 		data sent over the connection.  The protection mechanism
        /// 		takes effect immediately following the CRLF that concludes
        /// 		the authentication exchange for the client, and the CRLF of
        /// 		the positive response for the server.  Once the protection
        /// 		mechanism is in effect, the stream of command and response
        /// 		octets is processed into buffers of ciphertext.  Each
        /// 		buffer is transferred over the connection as a stream of
        /// 		octets prepended with a four octet field in network byte
        /// 		order that represents the length of the following data.
        /// 		The maximum ciphertext buffer length is defined by the
        /// 		protection mechanism.
        /// 
        /// 		The server is not required to support any particular
        /// 		authentication mechanism, nor are authentication mechanisms
        /// 		required to support any protection mechanisms.  If an AUTH
        /// 		command fails with a negative response, the session remains
        /// 		in the AUTHORIZATION state and client may try another
        /// 		authentication mechanism by issuing another AUTH command,
        /// 		or may attempt to authenticate by using the USER/PASS or
        /// 		APOP commands.  In other words, the client may request
        /// 		authentication types in decreasing order of preference,
        /// 		with the USER/PASS or APOP command as a last resort.
        /// 
        /// 		Should the client successfully complete the authentication
        /// 		exchange, the POP3 server issues a positive response and
        /// 		the POP3 session enters the TRANSACTION state.
        /// 				
        /// 		Examples:
        /// 			S: +OK POP3 server ready
        /// 			C: AUTH KERBEROS_V4
        /// 			S: + AmFYig==
        /// 			C: BAcAQU5EUkVXLkNNVS5FRFUAOCAsho84kLN3/IJmrMG+25a4DT
        /// 			   +nZImJjnTNHJUtxAA+o0KPKfHEcAFs9a3CL5Oebe/ydHJUwYFd
        /// 			   WwuQ1MWiy6IesKvjL5rL9WjXUb9MwT9bpObYLGOKi1Qh
        /// 			S: + or//EoAADZI=
        /// 			C: DiAF5A4gA+oOIALuBkAAmw==
        /// 			S: +OK Kerberos V4 authentication successful
        /// 			   ...
        /// 			C: AUTH FOOBAR
        /// 			S: -ERR Unrecognized authentication type
        /// </code>
        /// </remarks>
        private class AuthState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: CapaState

        /// <summary>
        ///     Represents the Pop3 protocol CAPA command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the CAPA command.
        ///     <code>
        ///  RFC 2449.5  The CAPA Command
        /// 	
        /// 		The POP3 CAPA command returns a list of capabilities supported by the
        /// 		POP3 server.  It is available in both the AUTHORIZATION and
        /// 		TRANSACTION states.
        /// 
        /// 		A capability description MUST document in which states the capability
        /// 		is announced, and in which states the commands are valid.
        /// 
        /// 		Capabilities available in the AUTHORIZATION state MUST be announced
        /// 		in both states.
        /// 
        /// 		If a capability is announced in both states, but the argument might
        /// 		differ after authentication, this possibility MUST be stated in the
        /// 		capability description.
        /// 
        /// 		(These requirements allow a client to issue only one CAPA command if
        /// 		it does not use any TRANSACTION-only capabilities, or any
        /// 		capabilities whose values may differ after authentication.)
        /// 
        /// 		If the authentication step negotiates an integrity protection layer,
        /// 		the client SHOULD reissue the CAPA command after authenticating, to
        /// 		check for active down-negotiation attacks.
        /// 
        /// 		Each capability may enable additional protocol commands, additional
        /// 		parameters and responses for existing commands, or describe an aspect
        /// 		of server behavior.  These details are specified in the description
        /// 		of the capability.
        /// 		
        /// 		Section 3 describes the CAPA response using [ABNF].  When a
        /// 		capability response describes an optional command, the &gt;capa-tag&lt;
        /// 		SHOULD be identical to the command keyword.  CAPA response tags are
        /// 		case-insensitive.
        /// 
        /// 		CAPA
        /// 
        /// 		Arguments:
        /// 			none
        /// 
        /// 		Restrictions:
        /// 			none
        /// 
        /// 		Discussion:
        /// 			An -ERR response indicates the capability command is not
        /// 			implemented and the client will have to probe for
        /// 			capabilities as before.
        /// 
        /// 			An +OK response is followed by a list of capabilities, one
        /// 			per line.  Each capability name MAY be followed by a single
        /// 			space and a space-separated list of parameters.  Each
        /// 			capability line is limited to 512 octets (including the
        /// 			CRLF).  The capability list is terminated by a line
        /// 			containing a termination octet (".") and a CRLF pair.
        /// 
        /// 		Possible Responses:
        /// 			+OK -ERR
        /// 
        /// 			Examples:
        /// 				C: CAPA
        /// 				S: +OK Capability list follows
        /// 				S: TOP
        /// 				S: USER
        /// 				S: SASL CRAM-MD5 KERBEROS_V4
        /// 				S: RESP-CODES
        /// 				S: LOGIN-DELAY 900
        /// 				S: PIPELINING
        /// 				S: EXPIRE 60
        /// 				S: UIDL
        /// 				S: IMPLEMENTATION Shlemazle-Plotz-v302
        /// 				S: .
        ///  </code>
        /// </remarks>
        private class CapaState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: Commands

        private class Commands
        {
            public const string USER = "USER";
            public const string PASS = "PASS";
            public const string STAT = "STAT";
            public const string LIST = "LIST";
            public const string RETR = "RETR";
            public const string DELE = "DELE";
            public const string NOOP = "NOOP";
            public const string RSET = "RSET";
            public const string QUIT = "QUIT";
            public const string UIDL = "UIDL";
            public const string APOP = "APOP";
            public const string TOP = "TOP";
            public const string AUTH = "AUTH";
            public const string CAPA = "CAPA";
            public const string STLS = "STLS";
        }

        #endregion

        #region Nested type: CommandState

        private abstract class CommandState
        {
            public Pop3CommandProcessor CommandProcessor { get; set; }

            public Pop3SessionProcessor SessionProcessor { get; set; }

            public abstract string Process(params string[] args);
        }

        #endregion

        #region Nested type: DeleState

        /// <summary>
        ///     Represents the Pop3 protocol DELE command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the DELE command.
        ///     <code>
        /// RFC 1939.5 DELE
        /// 	Arguments:
        /// 		a message-number (required) which may NOT refer to a
        /// 		message marked as deleted
        /// 	 
        /// 	Note:
        /// 		The POP3 server marks the message as deleted.  Any future
        /// 		reference to the message-number associated with the message
        /// 		in a POP3 command generates an error.  The POP3 server does
        /// 		not actually delete the message until the POP3 session
        /// 		enters the UPDATE state.
        /// </code>
        /// </remarks>
        private class DeleState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: ListState

        /// <summary>
        ///     Represents the Pop3 protocol LIST command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the LIST command.
        ///     <code>
        /// RFC 1939.5 LIST
        /// 	Arguments:
        /// 		a message-number (optional), which, if present, may NOT
        /// 		refer to a message marked as deleted
        /// 	 
        /// 	Note:
        /// 		If an argument was given and the POP3 server issues a
        /// 		positive response with a line containing information for
        /// 		that message.
        /// 
        /// 		If no argument was given and the POP3 server issues a
        /// 		positive response, then the response given is multi-line.
        /// 		
        /// 		Note that messages marked as deleted are not listed.
        /// 	
        /// 	Examples:
        /// 		C: LIST
        /// 		S: +OK 2 messages (320 octets)
        /// 		S: 1 120				
        /// 		S: 2 200
        /// 		S: .
        /// 		...
        /// 		C: LIST 2
        /// 		S: +OK 2 200
        /// 		...
        /// 		C: LIST 3
        /// 		S: -ERR no such message, only 2 messages in maildrop
        /// </code>
        /// </remarks>
        private class ListState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: NoopState

        /// <summary>
        ///     Represents the Pop3 protocol NOOP command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the NOOP command.
        ///     <code>
        /// RFC 1939.5 NOOP
        /// 	Note:
        /// 		The POP3 server does nothing, it merely replies with a
        /// 		positive response.
        /// </code>
        /// </remarks>
        private class NoopState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: PassState

        /// <summary>
        ///     Represents the Pop3 protocol PASS command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the PASS command.
        ///     <code>
        /// RFC 1939.7 PASS
        /// 	Arguments:
        /// 		A server/mailbox-specific password (required)
        /// 		
        /// 	Restrictions:
        /// 		May only be given in the AUTHORIZATION state immediately
        /// 		after a successful USER command
        /// 		
        /// 	Note:
        /// 		When the client issues the PASS command, the POP3 server
        /// 		uses the argument pair from the USER and PASS commands to
        /// 		determine if the client should be given access to the
        /// 		appropriate maildrop.
        /// 		
        /// 	Possible Responses:
        /// 		+OK maildrop locked and ready
        /// 		-ERR invalid password
        /// 		-ERR unable to lock maildrop
        /// </code>
        /// </remarks>
        private class PassState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: QuitState

        /// <summary>
        ///     Represents the Pop3 protocol QUIT command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the QUIT command.
        ///     <code>
        /// RFC 1939 QUIT
        /// 	Note:
        /// 		The POP3 server removes all messages marked as deleted
        /// 		from the maildrop and replies as to the status of this
        /// 		operation.  If there is an error, such as a resource
        /// 		shortage, encountered while removing messages, the
        /// 		maildrop may result in having some or none of the messages
        /// 		marked as deleted be removed.  In no case may the server
        /// 		remove any messages not marked as deleted.
        /// 
        /// 		Whether the removal was successful or not, the server
        /// 		then releases any exclusive-access lock on the maildrop
        /// 		and closes the TCP connection.
        /// </code>
        /// </remarks>
        private class QuitState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: RestState

        /// <summary>
        ///     Represents the Pop3 protocol RSET command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the RSET command.
        ///     <code>
        /// RFC 1939.5 RSET
        /// 	Discussion:
        /// 		If any messages have been marked as deleted by the POP3
        /// 		server, they are unmarked.  The POP3 server then replies
        /// 		with a positive response.
        /// </code>
        /// </remarks>
        private class RestState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: RetrState

        /// <summary>
        ///     Represents the Pop3 protocol RETR command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the RETR command.
        ///     <code>
        /// RFC 1939.5 RETR
        /// 	Arguments:
        /// 		a message-number (required) which may NOT refer to a
        /// 		message marked as deleted
        /// 	 
        /// 	Note:
        /// 		If the POP3 server issues a positive response, then the
        /// 		response given is multi-line.  After the initial +OK, the
        /// 		POP3 server sends the message corresponding to the given
        /// 		message-number, being careful to byte-stuff the termination
        /// 		character (as with all multi-line responses).
        /// 		
        /// 	Example:
        /// 		C: RETR 1
        /// 		S: +OK 120 octets
        /// 		S: [the POP3 server sends the entire message here]
        /// 		S: .
        /// </code>
        /// </remarks>
        private class RetrState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: StatState

        /// <summary>
        ///     Represents the Pop3 protocol STAT command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the STAT command.
        ///     <code>
        /// RFC 1939.5 STAT
        /// 	Note:
        /// 		The positive response consists of "+OK" followed by a single
        /// 		space, the number of messages in the maildrop, a single
        /// 		space, and the size of the maildrop in octets.
        /// 		
        /// 		Note that messages marked as deleted are not counted in
        /// 		either total.
        /// 	 
        /// 	Example:
        /// 		C: STAT
        /// 		S: +OK 2 320
        /// </code>
        /// </remarks>
        private class StatState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: StlsState

        /// <summary>
        ///     Represents the Pop3 protocol STLS command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the STLS command.
        ///     <code>
        /// RFC 2595.4 POP3 STARTTLS extension.
        ///          Arguments: none
        /// 
        ///          Restrictions:
        ///              Only permitted in AUTHORIZATION state.
        /// 
        ///          Discussion:
        ///              A TLS negotiation begins immediately after the CRLF at the
        ///              end of the +OK response from the server.  A -ERR response
        ///              MAY result if a security layer is already active.  Once a
        ///              client issues a STLS command, it MUST NOT issue further
        ///              commands until a server response is seen and the TLS
        ///              negotiation is complete.
        /// 
        ///              The STLS command is only permitted in AUTHORIZATION state
        ///              and the server remains in AUTHORIZATION state, even if
        ///              client credentials are supplied during the TLS negotiation.
        ///              The AUTH command [POP-AUTH] with the EXTERNAL mechanism
        ///              [SASL] MAY be used to authenticate once TLS client
        ///              credentials are successfully exchanged, but servers
        ///              supporting the STLS command are not required to support the
        ///              EXTERNAL mechanism.
        /// 
        ///              Once TLS has been started, the client MUST discard cached
        ///              information about server capabilities and SHOULD re-issue
        ///              the CAPA command.  This is necessary to protect against
        ///              man-in-the-middle attacks which alter the capabilities list
        ///              prior to STLS.  The server MAY advertise different
        ///              capabilities after STLS.
        /// 
        ///          Possible Responses:
        ///              +OK -ERR
        /// 
        ///          Examples:
        ///              C: STLS
        ///              S: +OK Begin TLS negotiation
        ///              <![CDATA[<TLS negotiation, further commands are under TLS layer>]]>
        ///               ...
        ///             C: STLS
        ///             S: -ERR Command not permitted when TLS active
        /// </code>
        /// </remarks>
        private class StlsState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: TopState

        /// <summary>
        ///     Represents the Pop3 protocol TOP command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the TOP command.
        ///     <code>
        /// RFC 1939.7 TOP
        /// 	Arguments:
        /// 		a message-number (required) which may NOT refer to to a
        /// 		message marked as deleted, and a non-negative number
        /// 		of lines (required)
        /// 
        /// 	Note:
        /// 		If the POP3 server issues a positive response, then the
        /// 		response given is multi-line.  After the initial +OK, the
        /// 		POP3 server sends the headers of the message, the blank
        /// 		line separating the headers from the body, and then the
        /// 		number of lines of the indicated message's body, being
        /// 		careful to byte-stuff the termination character (as with
        /// 		all multi-line responses).
        /// 	
        /// 	Examples:
        /// 		C: TOP 1 10
        /// 		S: +OK
        /// 		S: [the POP3 server sends the headers of the message,
        /// 		   a blank line, and the first 10 lines of the body of 
        /// 		   the message]
        /// 			
        /// 		S: .
        ///         ...
        /// 		C: TOP 100 3
        /// 		S: -ERR no such message
        /// </code>
        /// </remarks>
        private class TopState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: UildState

        /// <summary>
        ///     Represents the Pop3 protocol UIDL command.
        /// </summary>
        /// <remarks>
        ///     The following is an summary of the RFC documentation for the UIDL command.
        ///     <code>
        /// RFC 1939.7 UIDL
        /// 	Arguments:
        /// 	    a message-number (optional), which, if present, may NOT
        /// 		refer to a message marked as deleted
        /// 		
        /// 	Note:
        /// 		If an argument was given and the POP3 server issues a positive
        /// 		response, then the server returns a line containing information 
        ///         for that message.
        /// 
        /// 		If no argument was given and the POP3 server issues a positive
        /// 		response, then the response given is multi-line.  After the
        /// 		initial +OK, for each message in the maildrop, the POP3 server
        /// 		responds with a line containing information for that message.	
        /// 		
        /// 	Examples:
        /// 		C: UIDL
        /// 		S: +OK
        /// 		S: 1 whqtswO00WBw418f9t5JxYwZ
        /// 		S: 2 QhdPYR:00WBw1Ph7x7
        /// 		S: .
        /// 		...
        /// 		C: UIDL 2
        /// 		S: +OK 2 QhdPYR:00WBw1Ph7x7
        /// 		...
        /// 		C: UIDL 3
        /// 		S: -ERR no such message
        /// </code>
        /// </remarks>
        private class UildState : CommandState
        {
            public override string Process(params string[] args)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: UserState

        /// <summary>
        ///     Represents the Pop3 protocol USER command.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The USER command should only have one argument, the username.
        ///     </para>
        ///     <para>
        ///         The following is an summary of the RFC documentation for the USER command.
        ///         <code>
        /// RFC 1939.7 USER
        ///     Arguments:
        ///         A string identifying a mailbox (required), which is
        ///         of significance ONLY to the server
        /// 		
        ///     Note:
        ///         If the POP3 server responds with a positive status
        ///         indicator ("+OK"), then the client may issue either
        ///         the PASS command to complete the authentication, or
        ///         the QUIT command to terminate the POP3 session.
        /// </code>
        ///     </para>
        /// </remarks>
        private class UserState : CommandState
        {
            public override string Process(params string[] args)
            {
                // USER command should have only one argument.
                if (args.Length != 1)
                    return SR.Pop3ErrorUserNoUsername;

                if (CommandProcessor.UserName != null)
                    return SR.Pop3ErrorUsernameAlreadySpecfied;

                if (SessionProcessor.IsLoggedIn)
                    return string.Format(SR.Pop3ErrorUserAlreadyLoggedIn(args[0]));

                if (SessionProcessor.IsAuthenticated)
                    return SR.Pop3ErrorUserAlreadyAuthenticated;

                CommandProcessor.UserName = args[0];
                return SR.Pop3OkUsernameOk(CommandProcessor.UserName);
            }
        }

        #endregion
    }
}